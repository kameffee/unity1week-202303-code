using System;
using UniRx;
using Unity1week202303.TypingProgress;

namespace Unity1week202303.InGame.TypingProgress
{
    public class GetTypingResultUseCase
    {
        private readonly TypingProgressService _typingProgressService;

        public GetTypingResultUseCase(TypingProgressService typingProgressService)
        {
            _typingProgressService = typingProgressService;
        }

        public IObservable<int> OnFailedTyping()
        {
            return _typingProgressService.OnFailedTypingAsObservable();
        }

        public IObservable<int> OnSuccessTyping()
        {
            return _typingProgressService.OnUpdateProgressAsObservable()
                .Pairwise()
                .Where(pair => pair.Previous != null && pair.Current != null)
                // 成功文字数が増えているか
                .Select(pair =>
                {
                    if (pair.Previous.SentenceId == pair.Current.SentenceId)
                    {
                        if (pair.Previous.EnteredKanaText.Length < pair.Current.EnteredKanaText.Length)
                        {
                            return pair.Current.EnteredKanaText.Length - pair.Previous.EnteredKanaText.Length;
                        }

                        return 0;
                    }

                    return pair.Current.EnteredKanaText.Length;
                })
                .Where(count => count > 0);
        }
    }
}
