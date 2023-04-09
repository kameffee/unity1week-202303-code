using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1week202303.Audio
{
    public class AudioResourceLoader
    {
        private AudioResource _audioResource;

        public AudioResourceLoader(AudioResource audioResource)
        {
            _audioResource = audioResource;
        }

        public async UniTask<AudioClip> LoadAsync(int id)
        {
            if (!_audioResource.Valid(id))
            {
                throw new OperationCanceledException();
            }

            var audioClip = _audioResource.Get(id);
            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                audioClip.LoadAudioData();
            }

            await UniTask.WaitUntil(() => audioClip.loadState == AudioDataLoadState.Loaded);
            return audioClip;
        }
    }
}
