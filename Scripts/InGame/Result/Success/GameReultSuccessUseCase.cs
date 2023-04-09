using MessagePipe;

namespace Unity1week202303.InGame.Result.Success
{
    public class GameReultSuccessUseCase
    {
        private readonly IPublisher<SuccessData> _onResultSuccess;

        public GameReultSuccessUseCase(IPublisher<SuccessData> onResultSuccess)
        {
            _onResultSuccess = onResultSuccess;
        }

        public void In(SuccessData successData)
        {
            _onResultSuccess.Publish(successData);
        }
    }
}
