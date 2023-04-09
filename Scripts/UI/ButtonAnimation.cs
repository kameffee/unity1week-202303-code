using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

namespace Unity1week202303.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonAnimation : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.OnPointerDownAsObservable()
                .Subscribe(_ => OnPointerDown())
                .AddTo(this);
            _button.OnPointerUpAsObservable()
                .Subscribe(_ => OnPointerUp())
                .AddTo(this);
        }

        private void OnPointerDown()
        {
            transform.DOScale(0.95f, 0.15f)
                .Play();
        }
        
        private void OnPointerUp()
        {
            transform.DOScale(1f, 0.8f)
                .SetEase(Ease.OutElastic)
                .SetLink(gameObject)
                .Play();
        }
    }
}
