using Playground.Services.Log;
using UnityEngine;

namespace Playground.Services.Gameplay
{
    public abstract class GameplaySubService
    {
        #region Properties

        protected LevelModel LevelModel { get; private set; }

        #endregion

        #region Public methods

        public void Dispose()
        {
            OnDispose();
            LevelModel = null;
        }

        public void Initialize()
        {
            OnInitialize();
        }

        public void SetLevelModel(LevelModel levelModel)
        {
            LevelModel = levelModel;
        }

        #endregion

        #region Protected methods

        protected virtual void OnDispose() { }
        protected virtual void OnInitialize() { }

        #endregion
    }

    public abstract class GameplaySubService<TConfig> : GameplaySubService where TConfig : ScriptableObject
    {
        #region Variables

        private bool _isConfigLoaded;

        #endregion

        #region Properties

        protected TConfig Config { get; private set; }
        protected abstract string ConfigPath { get; }

        #endregion

        #region Protected methods

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (!_isConfigLoaded)
            {
                LoadConfig();
            }
        }

        #endregion

        #region Private methods

        private void LoadConfig()
        {
            Config = Resources.Load<TConfig>(ConfigPath);

            if (Config == null)
            {
                this.LogError($"[{nameof(LoadConfig)}] Can't load '{typeof(TConfig).Name}' config " +
                           $"for sub service '{GetType().Name}'");
            }

            _isConfigLoaded = true;
        }

        #endregion
    }
}