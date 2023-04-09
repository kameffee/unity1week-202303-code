using UnityEngine;

namespace Unity1week202303.InGame.Score
{
    public class ScoreCulculator
    {
        public GameScore Culculate(int success, int miss, int maxCombo, int time)
        {
            var speedScoreRatio = time / 60f;
            var accuracyRatio = success - (miss * 0.5f);
            var totalScore = Mathf.FloorToInt((success / speedScoreRatio) * accuracyRatio * 0.5f);
            return new GameScore(
                totalScore: totalScore,
                sendCount: success,
                failedCount: miss,
                maxComboCount: maxCombo
            );
        }
    }
}
