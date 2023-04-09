using System;
using DG.Tweening;
using Unity1week202303.Audio;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Unity1week202303.Actors
{
    public class ActorView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Transform _root;

        [SerializeField]
        private Transform _localRoot;

        [SerializeField]
        private Sprite[] _spriteList;

        [Header("Animator")]
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string[] _successTriggerNames;

        [SerializeField]
        private string[] _failedTriggerNames;

        public int Count => _spriteList.Length;

        public int CurrentIndex => _currentIndex;

        private int _currentIndex;
        private Tween _tween;

        private AudioPlayer _audioPlayer;

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void SetPose(int index)
        {
            _tween.Kill(true);

            var tempIndex = Mathf.Clamp(index, 0, _spriteList.Length - 1);

            _currentIndex = tempIndex;

            var sprite = _spriteList[_currentIndex];
            _spriteRenderer.sprite = sprite;

            Random.InitState(DateTime.Now.Millisecond);

            _audioPlayer.PlaySe(1);

            var dir = Random.Range(0, 100) % 2 == 0 ? 0.5f : -0.5f;
            _tween = _localRoot.DOJump(_localRoot.transform.position + new Vector3(dir, 0, 0), 1f, 1, 0.25f)
                .SetLink(gameObject)
                .Play();
        }

        public void SetReaction(bool isSuccess)
        {
            if (isSuccess)
            {
                var successTriggerName = _successTriggerNames[Random.Range(0, _successTriggerNames.Length)];
                _animator.SetTrigger(successTriggerName);
            }
            else
            {
                var failedTriggerName = _failedTriggerNames[Random.Range(0, _failedTriggerNames.Length)];
                _animator.SetTrigger(failedTriggerName);
            }
        }
    }
}
