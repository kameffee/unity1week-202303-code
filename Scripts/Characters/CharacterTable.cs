using System.Collections.Generic;
using System.Linq;

namespace Unity1week202303.Characters
{
    public class CharacterTable
    {
        private readonly List<Character> _characterList = new();

        public CharacterTable()
        {
            Register("あ", "a");
            Register("い", "i");
            Register("う", "u");
            Register("え", "e");
            Register("お", "o");

            Register("か", "ka");
            Register("き", "ki");
            Register("く", "ku");
            Register("け", "ke");
            Register("こ", "ko");

            Register("が", "ga");
            Register("ぎ", "gi");
            Register("ぐ", "gu");
            Register("げ", "ge");
            Register("ご", "go");

            Register("さ", "sa");
            Register("し", "si");
            Register("す", "su");
            Register("せ", "se");
            Register("そ", "so");

            Register("ざ", "za");
            Register("じ", "zi");
            Register("ず", "zu");
            Register("ぜ", "ze");
            Register("ぞ", "zo");

            Register("た", "ta");
            Register("ち", "ti");
            Register("つ", "tu");
            Register("つ", "tsu");
            Register("て", "te");
            Register("と", "to");

            Register("だ", "da");
            Register("ぢ", "di");
            Register("づ", "du");
            Register("で", "de");
            Register("ど", "do");

            Register("な", "na");
            Register("に", "ni");
            Register("ぬ", "nu");
            Register("ね", "ne");
            Register("の", "no");

            Register("は", "ha");
            Register("ひ", "hi");
            Register("ふ", "hu");
            Register("ふ", "fu");
            Register("へ", "he");
            Register("ほ", "ho");

            Register("ば", "ba");
            Register("び", "bi");
            Register("ぶ", "bu");
            Register("べ", "be");
            Register("ぼ", "bo");

            Register("ま", "ma");
            Register("み", "mi");
            Register("む", "mu");
            Register("め", "me");
            Register("も", "mo");

            Register("や", "ya");
            Register("ゆ", "yu");
            Register("よ", "yo");

            Register("ら", "ra");
            Register("り", "ri");
            Register("る", "ru");
            Register("れ", "re");
            Register("ろ", "ro");

            Register("わ", "wa");
            Register("を", "wo");
            Register("ん", "nn");

            Register("ぁ", "xa", true);
            Register("ぃ", "xi", true);
            Register("ぅ", "xu", true);
            Register("ぇ", "xe", true);
            Register("ぉ", "xo", true);

            Register("ぁ", "la", true);
            Register("ぃ", "li", true);
            Register("ぅ", "lu", true);
            Register("ぇ", "le", true);
            Register("ぉ", "lo", true);

            Register("っ", "ltu", true);
            Register("っ", "xtu", true);
            Register("っ", "ltsu", true);
            Register("っ", "xtsu", true);

            Register("きゃ", "kya");
            Register("きぃ", "kyi");
            Register("きゅ", "kyu");
            Register("きぇ", "kye");
            Register("きょ", "kyo");

            Register("くぁ", "qa");
            Register("くぃ", "qi");
            Register("くぅ", "qu");
            Register("くぇ", "qe");
            Register("くぉ", "qo");

            Register("ぎゃ", "gya");
            Register("ぎぃ", "gyi");
            Register("ぎゅ", "gyu");
            Register("ぎぇ", "gye");
            Register("ぎょ", "gyo");

            Register("ぐぁ", "gwa");
            Register("ぐぃ", "gwi");
            Register("ぐぅ", "gwu");
            Register("ぐぇ", "gwe");
            Register("ぐぉ", "gwo");

            Register("しゃ", "sya");
            Register("しぃ", "syi");
            Register("しゅ", "syu");
            Register("しぇ", "sye");
            Register("しょ", "syo");

            Register("しゃ", "sha");
            Register("し", "shi");
            Register("しゅ", "shu");
            Register("しぇ", "she");
            Register("しょ", "sho");

            Register("じゃ", "ja");
            Register("じ", "ji");
            Register("じゅ", "ju");
            Register("じぇ", "je");
            Register("じょ", "jo");

            Register("じゃ", "zya");
            Register("じ", "zyi");
            Register("じゅ", "zyu");
            Register("じぇ", "zye");
            Register("じょ", "zyo");

            Register("すぁ", "swa");
            Register("すぃ", "swi");
            Register("すぅ", "swu");
            Register("すぇ", "swe");
            Register("すぉ", "swo");

            Register("ちゃ", "cha");
            Register("ちゃ", "tya");
            Register("ち", "chi");
            Register("ちゅ", "chu");
            Register("ちゅ", "tyu");
            Register("ちぇ", "che");
            Register("ちぇ", "tye");
            Register("ちょ", "cho");
            Register("ちょ", "tyo");

            Register("ぢゃ", "dya");
            Register("ぢぃ", "dyi");
            Register("ぢゅ", "dyu");
            Register("ぢぇ", "dye");
            Register("ぢょ", "dyo");

            Register("てゃ", "tha");
            Register("てぃ", "thi");
            Register("てゅ", "thu");
            Register("てぇ", "the");
            Register("てょ", "tho");

            Register("でゃ", "dha");
            Register("でぃ", "dhi");
            Register("でゅ", "dhu");
            Register("でぇ", "dhe");
            Register("でょ", "dho");

            Register("とぁ", "twa");
            Register("とぃ", "twi");
            Register("とぅ", "twu");
            Register("とぇ", "twe");
            Register("とぉ", "two");

            Register("どぁ", "dwa");
            Register("どぃ", "dwi");
            Register("どぅ", "dwu");
            Register("どぇ", "dwe");
            Register("どぉ", "dwo");

            Register("にゃ", "nya");
            Register("にぃ", "nyi");
            Register("にゅ", "nyu");
            Register("にぇ", "nye");
            Register("にょ", "nyo");

            Register("ぱ", "pa");
            Register("ぴ", "pi");
            Register("ぷ", "pu");
            Register("ぺ", "pe");
            Register("ぽ", "po");

            Register("ひゃ", "hya");
            Register("ひぃ", "hyi");
            Register("ひゅ", "hyu");
            Register("ひぇ", "hye");
            Register("ひょ", "hyo");

            Register("ふぁ", "fa");
            Register("ふぁ", "fi");
            Register("ふゅ", "fyu");
            Register("ふぇ", "fe");
            Register("ふぉ", "fo");

            Register("みゃ", "mya");
            Register("みぃ", "myi");
            Register("みゅ", "myu");
            Register("みぇ", "mye");
            Register("みょ", "myo");

            Register("びゃ", "bya");
            Register("びぃ", "byi");
            Register("びゅ", "byu");
            Register("びぇ", "bye");
            Register("びょ", "byo");

            Register("ぴゃ", "pya");
            Register("ぴぃ", "pyi");
            Register("ぴゅ", "pyu");
            Register("ぴぇ", "pye");
            Register("ぴょ", "pyo");

            Register("みゃ", "mya");
            Register("みぃ", "myi");
            Register("みゅ", "myu");
            Register("みぇ", "mye");
            Register("みょ", "myo");

            Register("りゃ", "rya");
            Register("りぃ", "ryi");
            Register("りゅ", "ryu");
            Register("りぇ", "rye");
            Register("りょ", "ryo");

            Register("うぁ", "wha");
            Register("うぃ", "wi");
            Register("うぇ", "we");
            Register("うぉ", "who");

            Register("ゔぁ", "va");
            Register("ゔぃ", "vi");
            Register("ゔ", "vu");
            Register("ゔぇ", "ve");
            Register("ゔぉ", "vo");
            
            Register("ゃ", "lya", true);
            Register("ゅ", "lyu", true);
            Register("ょ", "lyo", true);
            
            Register("ゃ", "xya", true);
            Register("ゅ", "xyu", true);
            Register("ょ", "xyo", true);

            Register("ー", "-");
            Register("！", "!");
            Register("、", ",");
        }

        public IEnumerable<Character> All() => _characterList;

        public bool ExistByRoman(string roman)
        {
            return _characterList.Exists(character => character.Roman.Equals(roman));
        }

        public Character FindByRoman(string roman)
        {
            return _characterList.FirstOrDefault(character => character.Roman.Equals(roman));
        }

        public bool ExistByJpKana(string jpKana)
        {
            return _characterList.Exists(character => character.Jp.Equals(jpKana));
        }

        public IEnumerable<Character> FindByJpKana(string jpKana)
        {
            return _characterList.Where(character => character.Jp.Equals(jpKana));
        }

        public bool IsSmallWithoutTsu(char jpCharacter)
        {
            return IsSmallWithoutTsu(jpCharacter.ToString());
        }
        
        public bool IsSmallWithoutTsu(string jpCharacter)
        {
            return _characterList
                .Where(character => character.Jp.Equals(jpCharacter) && !character.Jp.Equals("っ"))
                .Any(character => character.IsSmall);
        }

        private void Register(string jp, string roman, bool isSmall = false)
        {
            _characterList.Add(new Character(jp, roman, isSmall));
        }
        
    }
}
