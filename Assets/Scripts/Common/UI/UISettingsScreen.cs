using System;
using Playground.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.Common.UI
{
    public class UISettingsScreen : UIScreen<UISettingsScreen.Model>
    {
        #region Public Nested Types

        public class Model
        {
            #region Variables

            public Action<float> ChangeSoundCallback;
            public float SoundVolume;

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private Slider _soundSlider;

        #endregion

        #region Protected methods

        protected override void OnHide()
        {
            base.OnHide();

            _soundSlider.onValueChanged.RemoveListener(OnSoundSliderChanged);
        }

        protected override void OnShow()
        {
            base.OnShow();

            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            _soundSlider.value = ScreenModel.SoundVolume;
        }

        #endregion

        #region Private methods

        private void OnSoundSliderChanged(float value)
        {
            ScreenModel.ChangeSoundCallback?.Invoke(value);
        }

        #endregion
    }
}