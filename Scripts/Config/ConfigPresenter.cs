using System;
using UniRx;
using Unity1week202303.Audio;
using VContainer.Unity;

namespace Unity1week202303.Config
{
    public class ConfigPresenter : IInitializable, IDisposable
    {
        private readonly AudioConfigView _audioConfigView;
        private readonly AudioSettingsService _audioSettingsService;
        private readonly AudioPlayer _audioPlayer;

        private readonly CompositeDisposable _disposable = new();

        public ConfigPresenter(
            AudioConfigView audioConfigView,
            AudioSettingsService audioSettingsService,
            AudioPlayer audioPlayer)
        {
            _audioConfigView = audioConfigView;
            _audioSettingsService = audioSettingsService;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _audioSettingsService.BgmVolume
                .Subscribe(volume => _audioConfigView.SetBgmVolume(volume.Value))
                .AddTo(_disposable);

            _audioSettingsService.SeVolume
                .Subscribe(volume => _audioConfigView.SetSeVolume(volume.Value))
                .AddTo(_disposable);

            _audioConfigView.OnChangeBgmVolumeAsObservable()
                .Subscribe(volume => _audioSettingsService.SetBgmVolume(new AudioVolume(volume)))
                .AddTo(_disposable);

            _audioConfigView.OnChangeSeVolumeAsObservable()
                .Subscribe(volume => _audioSettingsService.SetSeVolume(new AudioVolume(volume)))
                .AddTo(_disposable);

            _audioConfigView.OnPointerUpSeVolumeAsObservable()
                .Subscribe(_ => _audioPlayer.PlaySe(2))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
