using UnityEngine;

namespace Playground.Game.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _speedMultiplier = 1f;

        private Vector3 _mousePosition;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _mousePosition = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 offset = currentPosition - _mousePosition;

            transform.Rotate(new Vector3(0, offset.x / _speedMultiplier, 0));

            _mousePosition = currentPosition;
        }

        #endregion
    }
}