using NUnit.Framework;
using Unity1week202303.Characters;
using Unity1week202303.InGame.TypingProgress;

namespace Unity1week202303.Tests
{
    public class TypingValidatorTest
    {
        private TypingValidator _typingValidator;

        [SetUp]
        public void Setup()
        {
            var characterTable = new CharacterTable();
            _typingValidator = new TypingValidator(characterTable, new SentenceTranslator(new CharacterTranslator(characterTable)));
        }

        [TestCase("ふ", "", 'f')]
        [TestCase("ふ", "", 'h')]
        [TestCase("じ", "", 'j')]
        [TestCase("じ", "", 'z')]
        public void 複数パターンの入力がある文字(string taskJpSentence, string enteredRoman, char input)
        {
            Assert.That(_typingValidator.Validate(taskJpSentence, enteredRoman, input), Is.True);
        }
        
        [TestCase("しょ", "s", 'y')]
        [TestCase("しょ", "s", 'h')]
        [TestCase("じゃ", "", 'z')]
        [TestCase("じゃ", "z", 'y')]
        [TestCase("じゃ", "zy", 'a')]
        [TestCase("じゃ", "", 'j')]
        [TestCase("じゃ", "j", 'a')]
        public void 二文字で複数パターンの入力がある文字(string taskJpSentence, string enteredRoman, char input)
        {
            Assert.That(_typingValidator.Validate(taskJpSentence, enteredRoman, input), Is.True);
        }
    }
}
