using System;
using System.Collections.Generic;
using System.IO;
using Playground.DI;
using UnityEngine;


namespace Playground.Services.UI
{
    public class UIScreenProvider
    {
        #region Variables

        private const string ConfigPath = "Configs/UI/UIScreenConfig";

        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Type, BaseUIScreen> _prefabsByType = new();

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
            if (!_prefabsByType.TryGetValue(typeof(TScreen), out BaseUIScreen prefab))
            {
                prefab = LoadPrefab<TScreen>();
            }

            return _instantiator.InstantiatePrefab<TScreen>(prefab, layerTransform);
        }

        #endregion

        #region Private methods

        private bool IsNeededScreen<TScreen>(string screenPath) where TScreen : BaseUIScreen
        {
            return string.Equals(screenPath.Split("/")[^1], typeof(TScreen).Name);
        }

        private void LoadConfig()
        {
            _config = Resources.Load<UIScreenConfig>(ConfigPath);
        }

        private BaseUIScreen LoadPrefab<TScreen>() where TScreen : BaseUIScreen
        {
            foreach (string screenPath in _config.ScreenPath)
            {
                if (!IsNeededScreen<TScreen>(screenPath))
                {
                    continue;
                }

                TScreen prefab = Resources.Load<TScreen>(screenPath);
                _prefabsByType.Add(typeof(TScreen), prefab);

                return prefab;
            }

            Debug.LogError($"[{nameof(UIScreenProvider)}:{nameof(LoadPrefab)}] No prefab for screen " +
                           $"'{typeof(TScreen)}'");

            return null;
        }

        #endregion
    }
}