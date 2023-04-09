using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202303.InGame.Result.Success
{
    public class ResultSuccessView : MonoBehaviour
    {
        [SerializeField]
        private Button _retryButton;

        [SerializeField]
        private Button _rankingButton;

        [SerializeField]
        private List<ResultContentElement> _resultContentElements;

        public class ViewModel
        {
            public int SendScore { get; }
            public int MissCount { get; }
            public int MaxComboCount { get; }
            public int TotalScore { get; }

            public ViewModel(int totalScore, int sendScore, int missCount, int maxComboCount)
            {
                TotalScore = totalScore;
                SendScore = sendScore;
                MissCount = missCount;
                MaxComboCount = maxComboCount;
            }
        }

        private ViewModel _viewModel;

        private void Awake()
        {
            _resultContentElements.ForEach(element => element.Set(0));
            gameObject.SetActive(false);
            _retryButton.gameObject.SetActive(false);
            _rankingButton.gameObject.SetActive(false);
        }

        public async UniTask Show()
        {
            _resultContentElements.ForEach(element => element.Set(0));

            gameObject.SetActive(true);

            var scoreValues = new[]
            {
                _viewModel.SendScore,
                _viewModel.MissCount,
                _viewModel.MaxComboCount,
                _viewModel.TotalScore
            };
            
            for (var i = 0; i < scoreValues.Length; i++)
            {
                var scoreValue = scoreValues[i];
                var element = _resultContentElements[i]; 
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
                await element.Show();
                await element.Play(scoreValue);
            }
        }

        public async UniTask Hide()
        {
            gameObject.SetActive(false);
            _retryButton.gameObject.SetActive(false);
        }

        public void ShowRetryButton()
        {
            _retryButton.gameObject.SetActive(true);
        }

        public void ShowRankingButton()
        {
            _rankingButton.gameObject.SetActive(true);
        }

        public void ApplyViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IObservable<Unit> OnClickRetryAsObservable() => _retryButton.OnClickAsObservable();

        public IObservable<Unit> OnClickRankingAsObservable() => _rankingButton.OnClickAsObservable();
    }
}
