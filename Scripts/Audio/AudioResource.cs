using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity1week202303.Audio
{
    public class AudioResource : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _audioClips;

        public IEnumerable<AudioClip> AudioClips => _audioClips;

        public bool Valid(int id)
        {
            return 0 <= id && id < _audioClips.Length;
        }

        public AudioClip Get(int id)
        {
            return _audioClips.ElementAt(id);
        }
    }
}
