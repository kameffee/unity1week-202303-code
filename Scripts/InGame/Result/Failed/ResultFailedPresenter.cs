using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UniRx;
using Unity1week202303.Audio;
using Unity1week202303.InGame.Retry;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.InGame.Result.Failed
{
    public class ResultFailedPresenter : IInitializable, IDisposable
    {
        private readonly ISubscriber<FailedData> _onResultSuccess;
        private readonly Func<ResultFailedView> _viewFactory;
        private readonly RetryUseCase _retryUseCase;
        private readonly AudioPlayer _audioPlayer;
        private readonly CompositeDisposable _disposable = new();

        private CancellationTokenSource _cancellationTokenSource = new();
        private ResultFailedView _view;

        public ResultFailedPresenter(
            ISubscriber<FailedData> onResultSuccess,
            Func<ResultFailedView> viewFactory,
            RetryUseCase retryUseCase,
            AudioPlayer audioPlayer)
        {
            _onResultSuccess = onResultSuccess;
            _viewFactory = viewFactory;
            _retryUseCase = retryUseCase;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _view = _viewFactory.Invoke();
            _onResultSuccess
                .Subscribe(_ =>
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    ResultSequence(_cancellationTokenSource.Token).Forget();
                })
                .AddTo(_disposable);

            _view.OnClickRetryAsObservable()
                .Subscribe(_ =>
                {
                    _audioPlayer.PlaySe(2);
                    _retryUseCase.Retry();
                })
                .AddTo(_disposable);
        }

        private async UniTask ResultSequence(CancellationToken cancellationToken)
        {
            await _audioPlayer.StopBgm(0.5f);

            await _view.Show();

            // Retry or Space key
            await UniTask.WhenAny(
                _view.OnClickRetryAsObservable().ToUniTask(true, cancellationToken: cancellationToken),
                UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: cancellationToken)
            );

            await _view.Hide();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            _disposable?.Dispose();
        }
    }
}
