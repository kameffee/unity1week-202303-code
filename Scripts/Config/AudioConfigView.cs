using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202303.Config
{
    public class AudioConfigView : MonoBehaviour
    {
        [SerializeField]
        private Slider _bgmVolumeSlider;

        [SerializeField]
        private Slider _seVolumeSlider;

        public IObservable<float> OnChangeBgmVolumeAsObservable() => _bgmVolumeSlider.OnValueChangedAsObservable();

        public IObservable<float> OnChangeSeVolumeAsObservable() => _seVolumeSlider.OnValueChangedAsObservable();

        public IObservable<Unit> OnPointerUpSeVolumeAsObservable() => _seVolumeSlider.OnPointerUpAsObservable().AsUnitObservable();

        public void SetBgmVolume(float volume)
        {
            _bgmVolumeSlider.value = volume;
        }

        public void SetSeVolume(float volume)
        {
            _seVolumeSlider.value = volume;
        }
    }
}
