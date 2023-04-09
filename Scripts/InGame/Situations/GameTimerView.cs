using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unity1week202303.InGame.Situations
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _timeText;

        public void Set(TimeSpan timeSpan)
        {
            _timeText.SetText("{0:0}", (float)timeSpan.TotalSeconds);

            var sequence = DOTween.Sequence();
            sequence.Append(_timeText.rectTransform.DOScale(1.2f, 0.2f));
            sequence.Append(_timeText.rectTransform.DOScale(1f, 0.2f));
            sequence.SetLink(gameObject);
            sequence.Play();
        }
    }
}
