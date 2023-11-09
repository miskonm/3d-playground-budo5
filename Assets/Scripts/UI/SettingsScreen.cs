using System;
using Playground.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Playground.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Slider _soundSlider;

        private AudioService _audioService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(AudioService audioService)
        {
            _audioService = audioService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        }

        private void OnEnable()
        {
            _soundSlider.value = _audioService.SoundVolume;
        }

        #endregion

        #region Private methods

        private void OnSoundSliderChanged(float value)
        {
            _audioService.SetSoundVolume(value);
        }

        #endregion
    }
}