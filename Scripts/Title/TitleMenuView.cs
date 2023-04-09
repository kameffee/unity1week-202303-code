using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202303.Title
{
    public class TitleMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        public IObservable<Unit> OnClickStartAsObservable() => _startButton.OnClickAsObservable();
    }
}
