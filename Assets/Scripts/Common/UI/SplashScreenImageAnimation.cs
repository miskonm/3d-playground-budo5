using Playground.Services.Log;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.UI
{
    public class SplashScreenImageAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _image;
        [SerializeField] private float _circleTime = 1f;

        private float _fillSpeed;
        private float _timer;

        #endregion

        #region Properties

        private bool IsClockwise
        {
            get => _image.fillClockwise;
            set => _image.fillClockwise = value;
        }

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            
            IsClockwise = false;
            _fillSpeed = 1 / _circleTime;
            _image.fillMethod = Image.FillMethod.Radial360;
            _image.fillOrigin = (int)Image.Origin360.Top;
            _image.fillAmount = 1;
        }
        private void Update()
        {
            float speed = _fillSpeed * Time.deltaTime;
            _image.fillAmount += IsClockwise ? speed : -speed;
            if (_image.fillAmount == 0 || _image.fillAmount == 1)
            {
                IsClockwise = !IsClockwise;
            }
        }

        #endregion
    }
}