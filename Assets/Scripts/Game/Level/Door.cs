using DG.Tweening;
using UnityEngine;

namespace Playground.Game.Level
{
    public class Door : MonoBehaviour
    {
        #region Variables

        [Header("Animation")]
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _animationDuration;

        private Tween _tween;

        #endregion

        #region Public methods

        public void Close()
        {
            _tween?.Kill();
            _tween = transform
                .DOMove(_startPosition, _animationDuration)
                .SetUpdate(UpdateType.Fixed);
        }

        public void Open()
        {
            _tween?.Kill();
            _tween = transform
                .DOMove(_endPosition, _animationDuration)
                .SetUpdate(UpdateType.Fixed);
        }

        #endregion
    }
}