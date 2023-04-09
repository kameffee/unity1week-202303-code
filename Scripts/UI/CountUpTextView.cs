using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unity1week202303.UI
{
    public class CountUpTextView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _valueText;

        [SerializeField]
        private string _format = "{0:0}";

        [SerializeField]
        private Ease _ease = Ease.InOutSine;

        [SerializeField]
        private float _duration = 1f;

        private Tween _tween;
        private float _value;

        private void Awake()
        {
            if (string.IsNullOrEmpty(_format))
            {
                _format = "{0}";
            }
        }

        public void Set(float value)
        {
            _value = value;
            _valueText.SetText(_format, value);
        }

        public async UniTask Play(float endValue)
        {
            if (_tween != null && _tween.IsPlaying())
            {
                _tween.Kill();
            }

            _tween = DOTween.To(() => _value,
                    Set,
                    endValue,
                    _duration)
                .SetEase(_ease)
                .SetLink(gameObject);

            await _tween.Play();
        }
    }
}
