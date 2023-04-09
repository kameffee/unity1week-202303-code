using Unity1week202303.InGame.Combos;
using Unity1week202303.InGame.Situations;
using Unity1week202303.TypingProgress;
using UnityEngine;

namespace Unity1week202303.InGame.Score
{
    public class ScoreCulculateUseCase
    {
        private readonly Situation _situation;
        private readonly ComboCounter _comboCounter;
        private readonly ScoreCulculator _scoreCulculator;
        private readonly TimeCounter _timeCounter;

        public ScoreCulculateUseCase(
            Situation situation,
            ComboCounter comboCounter,
            ScoreCulculator scoreCulculator,
            TimeCounter timeCounter)
        {
            _situation = situation;
            _comboCounter = comboCounter;
            _scoreCulculator = scoreCulculator;
            _timeCounter = timeCounter;
        }

        public GameScore GetScore()
        {
            var gameScore = _scoreCulculator.Culculate(
                _situation.SendCount.Value,
                _situation.MissCount.Value,
                _comboCounter.MaxComboCount.Value,
                Mathf.FloorToInt(_timeCounter.ElapsedTime.Value)
            );

            return gameScore;
        }
    }
}
