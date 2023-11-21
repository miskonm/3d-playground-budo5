using Playground.Common.UI;
using Playground.Services.UI;

namespace Playground.Game
{
    public class GameScreenService
    {
        #region Variables

        private readonly UIService _uiService;
        private readonly SettingsScreenController _settingsScreenController;

        private UIGameScreen _screen;

        #endregion

        #region Setup/Teardown

        public GameScreenService(UIService uiService, SettingsScreenController settingsScreenController)
        {
            _uiService = uiService;
            _settingsScreenController = settingsScreenController;
        }

        #endregion

        #region Public methods

        public void HideScreen()
        {
            if (_screen != null)
            {
                _screen.Hide();
                _screen = null;
            }
        }

        public void ShowScreen()
        {
            _screen = _uiService.ShowScreen<UIGameScreen, UIGameScreen.Model>(new UIGameScreen.Model
            {
                NeedExitButton = false,
                SettingsButtonClickCallback = SettingsButtonClickCallback
            });
        }

        private void SettingsButtonClickCallback()
        {
            _settingsScreenController.ShowScreen();
        }

        #endregion
    }
}