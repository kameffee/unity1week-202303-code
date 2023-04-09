using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202303.InGame.Result.Failed
{
    public class ResultFailedView : MonoBehaviour
    {
        [SerializeField]
        private Button _retryButton;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
        }

        public async UniTask Hide()
        {
            gameObject.SetActive(false);
        }

        public IObservable<Unit> OnClickRetryAsObservable() => _retryButton.OnClickAsObservable();
    }
}
