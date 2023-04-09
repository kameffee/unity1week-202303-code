using Unity1week202303.Characters;

namespace Unity1week202303.Sentences
{
    public class SentenceData
    {
        public Identifier Id { get; }

        public string DisplayText { get; }
        
        public string KanaText { get; }
        
        public string AlphabetText { get; }
        
        public Character[] Characters { get; }

        public SentenceData(
            Identifier id, 
            string displayText,
            string kanaText,
            string alphabetText,
            Character[] characters)
        {
            Id = id;
            DisplayText = displayText;
            KanaText = kanaText;
            AlphabetText = alphabetText;
            Characters = characters;
        }
    }
}
