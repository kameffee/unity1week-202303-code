using System.Linq;
using Unity1week202303.Characters;
using UnityEngine;

namespace Unity1week202303.InGame.TypingProgress
{
    public class TypingValidator
    {
        private readonly CharacterTable _characterTable;
        private readonly SentenceTranslator _sentenceTranslator;

        public TypingValidator(
            CharacterTable characterTable,
            SentenceTranslator sentenceTranslator)
        {
            _characterTable = characterTable;
            _sentenceTranslator = sentenceTranslator;
        }
        
        /// <summary>
        /// Note: 必要なもの.
        /// 全体のひらがな, 入力途中のローマ字文字列
        /// </summary>
        /// <param name="taskJpSentence">タイピング課題のひらがな文字列</param>
        /// <param name="enteredRoman">入力済みローマ字</param>
        /// <param name="input">入力アルファベット</param>
        /// <returns>入力可能かを返す</returns>
        public bool Validate(string taskJpSentence, string enteredRoman, char input)
        {
            // 入力済み ひらがな文字列
            var enteredJpText= _sentenceTranslator.TranslateJpToRoman(enteredRoman);
            // 未入力 ひらがな文字列
            var notEnteredJpText = taskJpSentence.Substring(enteredJpText.Length);

            // 検索にかけたいひらがな文字
            var nextJpChar = notEnteredJpText[0].ToString();

            // 次の文字があるか
            var existNextChar = notEnteredJpText.Length >= 2;

            // 「○ぁ」や「○ょ」などか
            if (existNextChar && _characterTable.IsSmallWithoutTsu(notEnteredJpText[1]))
            {
                nextJpChar += notEnteredJpText[1];
            }

            // 次の候補算出
            var expectCharacters = _characterTable.FindByJpKana(nextJpChar).ToArray();
            // 入力可能なアルファベット
            var acceptableCharacterArray = expectCharacters
                .Where(character => character.Roman.StartsWith(enteredRoman))
                .Select(character => character.Roman[enteredRoman.Length])
                .ToArray();

            var isValid = acceptableCharacterArray.Any(c => c.Equals(input));
            Debug.Log($"[Valid] input:{input} acceptable: {new string(acceptableCharacterArray)}");
            return isValid;
        }

    }
}
