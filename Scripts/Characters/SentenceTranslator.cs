using System.Linq;
using System.Text;
using UnityEngine;

namespace Unity1week202303.Characters
{
    /// <summary>
    /// ローマ字から日本語に変換
    /// </summary>
    public class SentenceTranslator
    {
        private readonly CharacterTranslator _characterTranslator;

        public SentenceTranslator(CharacterTranslator characterTranslator)
        {
            _characterTranslator = characterTranslator;
        }

        /// <summary>
        /// 日本語ひらがなに変換
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        public string TranslateRomanToJp(string roman)
        {
            var result = new StringBuilder();
            var startIndex = 0;
            var length = 1;
            while ((startIndex + length) <= roman.Length)
            {
                var str = roman.Substring(startIndex, length);
                if (_characterTranslator.ExistByRoman(str))
                {
                    var characters = _characterTranslator.RomanToJpTranslate(str);
                    result.Append(characters.Jp);
                    startIndex += length;
                    length = 0;
                    continue;
                }

                // 「っ」判定
                if (str.Length == 2 && str[0] == str[1])
                {
                    result.Append("っ");

                    // 重複した1つ目のローマ字は無視したいのでインクリメント
                    startIndex++;
                    continue;
                }

                length++;
            }

            return result.ToString();
        }

        /// <summary>
        /// 日本語ひらがなからローマ字への変換
        /// </summary>
        /// <param name="jp"></param>
        /// <returns></returns>
        public string TranslateJpToRoman(string jp)
        {
            var result = new StringBuilder();
            var startIndex = 0;
            var length = 1;
            while ((startIndex + length) <= jp.Length)
            {
                var str = jp.Substring(startIndex, length);
                var characters = _characterTranslator.JpToRomanTranslate(str).ToArray();

                // 「っ」判定
                if (str.Length == 1 && str.Equals("っ"))
                {
                    var nextChar = jp[startIndex + 1];
                    var character = _characterTranslator.JpToRomanTranslate(nextChar.ToString()).First();
                    var firstChar = character.Roman[0];
                    result.Append(firstChar);
                    startIndex++;
                    length = 0;
                    continue;
                }

                // 1つ先の文字を見る
                // 「○ゅ」などの考慮のため
                if (startIndex + 1 < jp.Length)
                {
                    var nextChar = jp[startIndex + 1];
                    if (IsSmallChar(nextChar))
                    {
                        var twoChar = jp.Substring(startIndex, 2);
                        var twoCharacters = _characterTranslator.JpToRomanTranslate(twoChar).ToArray();
                        if (twoCharacters.Any())
                        {
                            result.Append(twoCharacters.First().Roman);
                            startIndex += 2;
                            length = 0;
                            continue;
                        }
                    }
                }

                if (characters.Any())
                {
                    result.Append(characters.First().Roman);
                    startIndex += length;
                    length = 0;
                }
                else
                {
                    length++;
                }
            }

            return result.ToString();
        }

        // TODO: 別クラスへ持たせたい
        private bool IsSmallChar(char character)
        {
            return character is 'ぁ' or 'ぃ' or 'ぅ' or 'ぉ' or 'ゃ' or 'ゅ' or 'ょ';
        }
    }
}
