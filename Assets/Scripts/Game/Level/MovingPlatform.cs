using System;
using DG.Tweening;
using UnityEngine;

namespace Playground.Game.Level
{
    public class MovingPlatform : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _endPoint;
        [SerializeField] private float _moveDuration = 3;
        [SerializeField] private float _moveDelay = 1;

        private Tween _tween;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            transform.position = _startPoint;
        }

        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(_moveDelay);
            sequence.Append(transform.DOMove(_endPoint, _moveDuration));
            sequence.AppendInterval(_moveDelay);
            sequence.Append(transform.DOMove(_startPoint, _moveDuration));
            sequence.SetLoops(-1);
            sequence.SetUpdate(UpdateType.Fixed);
            _tween = sequence;
        }

        private void OnDestroy()
        {
            _tween?.Kill();
            _tween = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_startPoint, 0.3f);
            Gizmos.DrawSphere(_endPoint, 0.3f);
            Gizmos.DrawLine(_startPoint, _endPoint);
        }

        #endregion
    }
}