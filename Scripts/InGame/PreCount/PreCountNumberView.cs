using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202303.InGame.PreCount
{
    public class PreCountNumberView : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = transform as RectTransform;
            gameObject.SetActive(false);
        }

        public async UniTask Play()
        {
            gameObject.SetActive(true);
            rectTransform.eulerAngles = new Vector3(0, 0, 45f);
            rectTransform.localScale = new Vector3(0.5f, 0.5f, 1f);
            var sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOScale(1, 0.2f)
                .SetEase(Ease.OutBack)
            );
            sequence.Join(rectTransform.DORotate(Vector3.zero, 0.4f)
                .SetEase(Ease.OutBack)
            );
            sequence.SetLink(gameObject);
            await sequence.Play();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
