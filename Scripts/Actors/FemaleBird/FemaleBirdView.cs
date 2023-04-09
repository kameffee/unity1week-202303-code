using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202303.Actors.FemaleBird
{
    public class FemaleBirdView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Transform _showPosition;

        [SerializeField]
        private Transform _hidePosition;

        [Header("Effect")]
        [SerializeField]
        private ParticleSystem _heartParticle;

        [SerializeField]
        private ParticleSystem _breakHeartParticle;

        private void Awake()
        {
            transform.position = _hidePosition.position;
            _spriteRenderer.enabled = true;
        }

        public async UniTask Show()
        {
            _spriteRenderer.enabled = true;
            await transform.DOJump(_showPosition.position, 1, 5, 1f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .Play();
        }

        public async UniTask Hide()
        {
            transform.localScale = new Vector3(-1, 1, 1);

            await transform.DOJump(_hidePosition.position, 1, 5, 1f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .Play();
            _spriteRenderer.enabled = false;

            transform.localScale = Vector3.one;
        }

        public void EmitHeart(int count)
        {
            _heartParticle.Emit(count);
        }

        public void EmitBreakHeart(int count)
        {
            _breakHeartParticle.Emit(count);
        }
    }
}
