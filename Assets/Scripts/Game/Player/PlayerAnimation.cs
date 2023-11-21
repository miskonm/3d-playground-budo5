using UnityEngine;
using UnityEngine.Scripting;

namespace Playground.Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Variables

        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int SpeedX = Animator.StringToHash("SpeedX");
        private static readonly int SpeedY = Animator.StringToHash("SpeedY");
        private static readonly int SpeedZ = Animator.StringToHash("SpeedZ");

        [SerializeField] private Animator _animator;

        #endregion

        #region Public methods

        public void SetGrounded(bool isGrounded)
        {
            _animator.SetBool(IsGrounded, isGrounded);
        }

        public void SetSpeed(Vector3 speed)
        {
            _animator.SetFloat(Speed, speed.magnitude);
            _animator.SetFloat(SpeedX, speed.x);
            _animator.SetFloat(SpeedZ, speed.z);
        }

        public void SetSpeedY(float speed)
        {
            _animator.SetFloat(SpeedY, speed);
        }
        
        [Preserve]
        public void OnAnimation()
        {
            
        }

        #endregion
    }
}