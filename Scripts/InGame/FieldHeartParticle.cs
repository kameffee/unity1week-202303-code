using UnityEngine;

namespace Unity1week202303.InGame
{
    public class FieldHeartParticle : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        public void Play()
        {
            _particleSystem.Play();
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }
    }
}
