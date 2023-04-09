using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202303.Actors.FemaleBird;
using Unity1week202303.Audio;
using Unity1week202303.InGame.Combos;
using Unity1week202303.InGame.Inputs;
using Unity1week202303.InGame.PreCount;
using Unity1week202303.InGame.Ready;
using Unity1week202303.InGame.Result.Failed;
using Unity1week202303.InGame.Result.Success;
using Unity1week202303.InGame.Retry;
using Unity1week202303.InGame.Situations;
using Unity1week202303.InGame.State;
using Unity1week202303.InGame.TypingTasks;
using Unity1week202303.Sentences;
using Unity1week202303.TypingProgress;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.InGame
{
    public class InGameLoop : IAsyncStartable, IInitializable, IDisposable
    {
        private readonly TypingProgressService _typingProgressService;
        private readonly TypingTaskUseCase _typingTaskUseCase;
        private readonly GameReultSuccessUseCase _gameReultSuccessUseCase;
        private readonly GameResultFailedUseCase _gameResultFailedUseCase;
        private readonly GameReadyPresenter _gameReadyPresenter;
        private readonly InGameStateUseCase _inGameStateUseCase;
        private readonly PreCountPresenter _preCountPresenter;
        private readonly TimeCounter _timeCounter;
        private readonly RemainingTimer _remainingTimer;
        private readonly AudioPlayer _audioPlayer;
        private readonly RetryUseCase _retryUseCase;
        private readonly ComboCounter _comboCounter;
        private readonly Situation _situation;
        private readonly SentenceRepository _sentenceRepository;
        private readonly FemaleBirdPresenter _femaleBirdPresenter;
        private readonly ForceInputFieldModeUseCase _forceInputFieldModeUseCase;
        private readonly CompositeDisposable _disposable = new();

        private CancellationTokenSource _cancellationTokenSource = new();

        public InGameLoop(
            TypingProgressService typingProgressService,
            TypingTaskUseCase typingTaskUseCase,
            GameReultSuccessUseCase gameReultSuccessUseCase,
            GameResultFailedUseCase gameResultFailedUseCase,
            GameReadyPresenter gameReadyPresenter,
            InGameStateUseCase inGameStateUseCase,
            PreCountPresenter preCountPresenter,
            TimeCounter timeCounter,
            RemainingTimer remainingTimer,
            AudioPlayer audioPlayer,
            RetryUseCase retryUseCase,
            ComboCounter comboCounter,
            Situation situation,
            SentenceRepository sentenceRepository,
            FemaleBirdPresenter femaleBirdPresenter,
            ForceInputFieldModeUseCase forceInputFieldModeUseCase
        )
        {
            _typingProgressService = typingProgressService;
            _typingTaskUseCase = typingTaskUseCase;
            _gameReultSuccessUseCase = gameReultSuccessUseCase;
            _gameResultFailedUseCase = gameResultFailedUseCase;
            _gameReadyPresenter = gameReadyPresenter;
            _inGameStateUseCase = inGameStateUseCase;
            _preCountPresenter = preCountPresenter;
            _timeCounter = timeCounter;
            _remainingTimer = remainingTimer;
            _audioPlayer = audioPlayer;
            _retryUseCase = retryUseCase;
            _comboCounter = comboCounter;
            _situation = situation;
            _sentenceRepository = sentenceRepository;
            _femaleBirdPresenter = femaleBirdPresenter;
            _forceInputFieldModeUseCase = forceInputFieldModeUseCase;
        }

        public void Initialize()
        {
            // リトライ検出
            _retryUseCase.OnRetry()
#if DEBUG
                .Merge(Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Slash)).AsUnitObservable())
#endif
                .Subscribe(_ => StartGame(_cancellationTokenSource.Token).Forget())
                .AddTo(_disposable)
                .AddTo(_cancellationTokenSource.Token);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            // 最初のセットアップ
            await _sentenceRepository.LoadAsync();

            await StartGame(cancellation);
        }

        private async UniTask StartGame(CancellationToken cancellation)
        {
            _forceInputFieldModeUseCase.SetEnable(true);

            _comboCounter.Clear();
            _timeCounter.Reset();
            _situation.Reset();
            await _femaleBirdPresenter.Hide();

            await _audioPlayer.PlayBgm(0);

            _remainingTimer.SetTime(TimeSpan.FromSeconds(60));

            _inGameStateUseCase.SetState(InGameStateType.Ready);
            await _gameReadyPresenter.Show();

            _typingTaskUseCase.Set();

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: cancellation);

            await _gameReadyPresenter.Hide();

            // 雌鳥登場
            await _femaleBirdPresenter.Show();

            // PreCount
            _inGameStateUseCase.SetState(InGameStateType.PreCount);
            await _preCountPresenter.Play(cancellation);

            _inGameStateUseCase.SetState(InGameStateType.Playing);

            _timeCounter.Start();

            while (!cancellation.IsCancellationRequested && _typingTaskUseCase.HasNext())
            {
                var nextTypingWorkTask = _typingTaskUseCase.Next();
                _typingProgressService.SetTaskWord(nextTypingWorkTask.Sentence);

                UniTask[] tasks = new UniTask[]
                {
                    _typingProgressService.OnCompleted().ToUniTask(true, cancellation),
                    _remainingTimer.OnFinish.ToUniTask(true, cancellation),
                    _situation.MissCount.Where(missCount => missCount >= 100)
                        .First()
                        .ToUniTask(true, cancellationToken: cancellation)
                };
                var result = await UniTask.WhenAny(tasks);
                if (result == 1)
                {
                    break;
                }
                else if (result >= 2)
                {
                    await GameEnd(false, cancellation);
                    return;
                }
            }

            await GameEnd(true, cancellation);
        }

        private async UniTask GameEnd(bool isSuccess, CancellationToken cancellationToken)
        {
            _timeCounter.Stop();

            _inGameStateUseCase.SetState(InGameStateType.Result);
            if (isSuccess)
            {
                _gameReultSuccessUseCase.In(new SuccessData());
            }
            else
            {
                await _femaleBirdPresenter.Hide();
                _gameResultFailedUseCase.In(new FailedData());
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cancellationTokenSource?.Dispose();
        }
    }
}
