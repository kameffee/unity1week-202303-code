using NUnit.Framework;
using Unity1week202303.InGame.Score;
using UnityEngine;

namespace Unity1week202303.Tests
{
    public class ScoreCulculatorTest
    {
        private readonly ScoreCulculator _scoreCulculator = new ScoreCulculator();

        [TestCase(100, 0, 100, 60)]
        [TestCase(100, 10, 5, 60)]
        [TestCase(100, 10, 5, 50)]
        public void テスト(int sendCount, int missCount, int maxCombo, int time)
        {
            Debug.Log(_scoreCulculator.Culculate(sendCount, missCount, maxCombo, time).ToString());
        }
    }
}
