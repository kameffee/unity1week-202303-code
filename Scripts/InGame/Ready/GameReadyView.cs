using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202303.InGame.Ready
{
    public class GameReadyView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private Tween _tween;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0.2f, 1f)
                .SetEase(Ease.InSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject)
                .Play();
        }

        public async UniTask Hide()
        {
            gameObject.SetActive(false);
            _tween?.Kill();
        }
    }
}
