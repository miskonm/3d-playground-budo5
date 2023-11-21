using System;
using Playground.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.Common.UI
{
    public class UIGameScreen : UIScreen<UIGameScreen.Model>
    {
        public class Model
        {
            public bool NeedExitButton;
            public Action SettingsButtonClickCallback;
            public Action ExitButtonClickCallback;
        }

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _exitButton.gameObject.SetActive(ScreenModel.NeedExitButton);
        }

        protected override void OnShow()
        {
            base.OnShow();
            
            _settingsButton.onClick.AddListener(OnSettingsClicked);
            _exitButton.onClick.AddListener(OnExitClicked);

            ScreenModel.SettingsButtonClickCallback = null;
            ScreenModel.ExitButtonClickCallback = null;
        }

        protected override void OnHide()
        {
            base.OnHide();
            
            _settingsButton.onClick.RemoveListener(OnSettingsClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }

        private void OnExitClicked()
        {
            ScreenModel.ExitButtonClickCallback?.Invoke();
        }

        private void OnSettingsClicked()
        {
            ScreenModel.SettingsButtonClickCallback?.Invoke();
        }
    }
}