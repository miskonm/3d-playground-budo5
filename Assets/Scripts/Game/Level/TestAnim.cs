using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game.Level
{
    public class TestAnim : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _middlePoint;
        [SerializeField] private float _toMiddleDuration = 2;
        [SerializeField] private Vector3 _endPoint;
        [SerializeField] private float _toEndDuration = 2;
        
        
        [Button()]
        public void Play()
        {
            PlayAsync().Forget();
        }

        private async UniTask PlayAsync()
        {
            Debug.LogError($"PlayAsync '{Time.frameCount}'");
            await transform.DOMove(_middlePoint, _toMiddleDuration).From(_startPoint);
            // wait
            Debug.LogError($"Perform middle actions '{Time.frameCount}'");
            await  transform.DOMove(_endPoint, _toEndDuration);
            // wait
            Debug.LogError($"Perform end actions '{Time.frameCount}'");
        }
    }
}