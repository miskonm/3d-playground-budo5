using UnityEngine;
using Zenject;

namespace Playground.Services.UI
{
    public class UIScreenProvider
    {
        #region Variables

        private const string ConfigPath = "Configs/UI/UIScreenConfig";
        
        private readonly IInstantiator _instantiator;

        private UIScreenConfig _config;

        #endregion

        #region Setup/Teardown

        public UIScreenProvider(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        #endregion

        #region Public methods

        public void Bootstrap()
        {
            LoadConfig();
        }

        public TScreen GetScreen<TScreen>(Transform layerTransform) where TScreen : BaseUIScreen
        {
            foreach (BaseUIScreen uiScreen in _config.Screen)
            {
                if (!IsNeededScreen<TScreen>(uiScreen))
                {
                    continue;
                }

                return _instantiator.InstantiatePrefabForComponent<TScreen>(uiScreen, layerTransform);
            }

            Debug.LogError($"[{nameof(UIScreenProvider)}:{nameof(GetScreen)}] No prefab for screen " +
                           $"'{typeof(TScreen)}'");

            return null;
        }

        #endregion

        #region Private methods

        private bool IsNeededScreen<TScreen>(BaseUIScreen uiScreen) where TScreen : BaseUIScreen
        {
            return string.Equals(uiScreen.name, typeof(TScreen).Name);
        }

        private void LoadConfig()
        {
            _config = Resources.Load<UIScreenConfig>(ConfigPath);
        }

        #endregion
    }
}