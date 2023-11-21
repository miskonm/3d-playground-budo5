using Playground.Common.UI;
using Playground.Services.Audio;
using Playground.Services.UI;

namespace Playground.Game
{
    public class SettingsScreenController
    {
        #region Variables

        private readonly AudioService _audioService;
        private readonly UIService _uiService;

        #endregion

        #region Setup/Teardown

        public SettingsScreenController(UIService uiService, AudioService audioService)
        {
            _uiService = uiService;
            _audioService = audioService;
        }

        #endregion

        #region Public methods

        public void ShowScreen()
        {
            _uiService.ShowScreen<UISettingsScreen, UISettingsScreen.Model>(new UISettingsScreen.Model
            {
                SoundVolume = _audioService.SoundVolume,
                ChangeSoundCallback = ChangeSoundCallback,
            });
        }

        #endregion

        #region Private methods

        private void ChangeSoundCallback(float volume)
        {
            _audioService.SetSoundVolume(volume);
        }

        #endregion
    }
}