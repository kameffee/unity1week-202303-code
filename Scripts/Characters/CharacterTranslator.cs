using System.Collections.Generic;
using System.Linq;

namespace Unity1week202303.Characters
{
    public class CharacterTranslator
    {
        private CharacterTable _characterTable;

        public CharacterTranslator(CharacterTable characterTable)
        {
            _characterTable = characterTable;
        }
        
        public bool ExistByJp(string jpCharacter)
        {
            return _characterTable.ExistByJpKana(jpCharacter);
        }

        public IEnumerable<Character> JpToRomanTranslate(string jpCharacter)
        {
            return _characterTable.FindByJpKana(jpCharacter);
        }

        public bool ExistByRoman(string roman)
        {
            return _characterTable.ExistByRoman(roman);
        }

        public Character RomanToJpTranslate(string romanCharacter)
        {
            return _characterTable.FindByRoman(romanCharacter);
        }
    }
}
