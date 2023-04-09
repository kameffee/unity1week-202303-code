namespace Unity1week202303.Characters
{
    public struct Character
    {
        /// <summary>
        /// 日本語文字
        /// </summary>
        public string Jp { get; }

        /// <summary>
        /// ローマ字
        /// </summary>
        public string Roman { get; }

        /// <summary>
        /// 「っ」や「ぁ」など
        /// </summary>
        public bool IsSmall { get; }

        public Character(string jp, string roman, bool isSmall = false)
        {
            Jp = jp;
            Roman = roman;
            IsSmall = isSmall;
        }
        
        public bool IsValid() => !Jp.Equals(string.Empty) && !Roman.Equals(string.Empty);

        public static Character Empty => new(string.Empty, string.Empty);
    }
}
