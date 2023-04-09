namespace Unity1week202303.InGame.Score
{
    public class GameScore
    {
        public int TotalScore { get; }

        public int SendCount { get; }

        public int FailedCount { get; }

        public int MaxComboCount { get; }

        public GameScore(int totalScore, int sendCount, int failedCount, int maxComboCount)
        {
            TotalScore = totalScore;
            SendCount = sendCount;
            FailedCount = failedCount;
            MaxComboCount = maxComboCount;
        }

        public override string ToString()
        {
            return $"{nameof(TotalScore)}: {TotalScore}, {nameof(SendCount)}: {SendCount}, {nameof(FailedCount)}: {FailedCount}, {nameof(MaxComboCount)}: {MaxComboCount}";
        }
    }
}
