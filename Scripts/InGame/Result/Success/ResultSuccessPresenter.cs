using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using naichilab;
using UniRx;
using Unity1week202303.Audio;
using Unity1week202303.InGame.Inputs;
using Unity1week202303.InGame.Retry;
using Unity1week202303.InGame.Score;
using Unity1week202303.Scenes;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.InGame.Result.Success
{
    public class ResultSuccessPresenter : IInitializable, IDisposable
    {
        private readonly ISubscriber<SuccessData> _onResultSuccess;
        private readonly Func<ResultSuccessView> _viewFactory;
        private readonly RetryUseCase _retryUseCase;
        private readonly ScoreCulculateUseCase _scoreCulculateUseCase;
        private readonly SceneManagerEvents _sceneManagerEvents;
        private readonly ForceInputFieldModeUseCase _forceInputFieldModeUseCase;
        private readonly AudioPlayer _audioPlayer;
        private readonly FieldHeartParticle _fieldHeartParticle;

        private readonly CompositeDisposable _disposable = new();

        private CancellationTokenSource _cancellationTokenSource;
        private ResultSuccessView _view;

        public ResultSuccessPresenter(
            ISubscriber<SuccessData> onResultSuccess,
            Func<ResultSuccessView> viewFactory,
            RetryUseCase retryUseCase,
            ScoreCulculateUseCase scoreCulculateUseCase,
            SceneManagerEvents sceneManagerEvents,
            ForceInputFieldModeUseCase forceInputFieldModeUseCase,
            AudioPlayer audioPlayer,
            FieldHeartParticle fieldHeartParticle)
        {
            _onResultSuccess = onResultSuccess;
            _viewFactory = viewFactory;
            _retryUseCase = retryUseCase;
            _scoreCulculateUseCase = scoreCulculateUseCase;
            _sceneManagerEvents = sceneManagerEvents;
            _forceInputFieldModeUseCase = forceInputFieldModeUseCase;
            _audioPlayer = audioPlayer;
            _fieldHeartParticle = fieldHeartParticle;
        }

        public void Initialize()
        {
            _view = _viewFactory.Invoke();

            _onResultSuccess
                .Subscribe(successData =>
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    ResultSequence(successData, _cancellationTokenSource.Token).Forget();
                })
                .AddTo(_disposable);

            _view.OnClickRetryAsObservable()
                .Subscribe(_ => _audioPlayer.PlaySe(2))
                .AddTo(_disposable);

            _view.OnClickRankingAsObservable()
                .Subscribe(_ =>
                {
                    var score = _scoreCulculateUseCase.GetScore();
                    RankingLoader.Instance.SendScoreAndShowRanking(score.TotalScore);
                })
                .AddTo(_disposable);
        }

        private async UniTask ResultSequence(SuccessData successData, CancellationToken cancellationToken)
        {
            await _audioPlayer.StopBgm(0.5f);
            await _audioPlayer.PlayBgm(4);

            var score = _scoreCulculateUseCase.GetScore();
            var viewModel = new ResultSuccessView.ViewModel(
                totalScore: score.TotalScore,
                sendScore: score.SendCount,
                missCount: score.FailedCount,
                maxComboCount: score.MaxComboCount
            );

            _view.ApplyViewModel(viewModel);

            await _view.Show();

            _fieldHeartParticle.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            // ランキングの文字入力ができるようにするため
            _forceInputFieldModeUseCase.SetEnable(false);

            // ランキング送信
            RankingLoader.Instance.SendScoreAndShowRanking(score.TotalScore);

            // ランキングがアンロードされるまで待つ
            await _sceneManagerEvents.OnUnloadedAsObservable()
                .Where(scene => scene.buildIndex == (int)SceneDefine.Ranking)
                .ToUniTask(true, cancellationToken);

            _view.ShowRetryButton();
            _view.ShowRankingButton();

            // Retry or Space key
            await UniTask.WhenAny(
                _view.OnClickRetryAsObservable().ToUniTask(true, cancellationToken),
                UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: cancellationToken)
            );

            _fieldHeartParticle.Stop();
            await _view.Hide();

            _retryUseCase.Retry();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
