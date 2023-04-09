using MessagePipe;

namespace Unity1week202303.InGame.Result.Failed
{
    public class GameResultFailedUseCase
    {
        private readonly IPublisher<FailedData> _onResultFailed;

        public GameResultFailedUseCase(IPublisher<FailedData> onResultFailed)
        {
            _onResultFailed = onResultFailed;
        }

        public void In(FailedData failedData)
        {
            _onResultFailed.Publish(failedData);
        }
    }
}
