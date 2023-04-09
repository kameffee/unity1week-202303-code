using Unity1week202303.Sentences;

namespace Unity1week202303.TypingProgress
{
    public class TypingProgressData
    {
        public Identifier SentenceId { get; }
        
        public string Word => Entered + NotEntered;

        public string Entered { get; }
        public string NotEntered { get; }
        public string EnteredKanaText { get; }
        public string NotEnteredKanaText { get; }

        public TypingProgressData(
            Identifier sentenceId,
            string entered,
            string notEntered,
            string enteredKanaText,
            string notEnteredKanaText)
        {
            SentenceId = sentenceId;
            Entered = entered;
            NotEntered = notEntered;
            EnteredKanaText = enteredKanaText;
            NotEnteredKanaText = notEnteredKanaText;
        }

        public bool IsCompleted() => Word.Length == Entered.Length && NotEntered.Length == 0;
    }
}
