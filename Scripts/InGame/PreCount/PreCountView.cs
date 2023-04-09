using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1week202303.InGame.PreCount
{
    public class PreCountView : MonoBehaviour
    {
        [SerializeField]
        private List<PreCountNumberView> _countList;

        public async UniTask Play(CancellationToken cancellationToken)
        {
            for (int index = 0; index < _countList.Count; index++)
            {
                var preCountNumberView = _countList[index];
                await preCountNumberView.Play();

                await UniTask.Delay(TimeSpan.FromSeconds(0.4f), cancellationToken: cancellationToken);

                preCountNumberView.Hide();
            }
        }
    }
}
