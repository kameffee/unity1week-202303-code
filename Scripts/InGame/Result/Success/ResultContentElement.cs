using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity1week202303.UI;
using UnityEngine;

namespace Unity1week202303.InGame.Result.Success
{
    public class ResultContentElement : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private CountUpTextView _countUpTextView;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public async UniTask Show()
        {
            await _canvasGroup
                .DOFade(1, 0.2f)
                .Play();
        }

        public void Set(float value)
        {
            _countUpTextView.Set(value);
        }

        public async UniTask Play(float endValue)
        {
            await _countUpTextView.Play(endValue);
        }
    }
}
