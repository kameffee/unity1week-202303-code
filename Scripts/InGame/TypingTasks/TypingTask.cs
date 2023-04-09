using Unity1week202303.Sentences;

namespace Unity1week202303.InGame.TypingTasks
{
    public class TypingTask
    {
        public int Number { get; }

        public SentenceData Sentence { get; }

        public TypingTask(int number, SentenceData sentence)
        {
            Number = number;
            Sentence = sentence;
        }
    }
}
