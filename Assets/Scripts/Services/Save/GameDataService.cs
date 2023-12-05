using System;
using System.IO;
using Playground.DI;
using Playground.Services.AppState;
using Playground.Services.Event;
using Playground.Services.Log;
using UnityEngine;

namespace Playground.Services.Save
{
    public class GameDataService : MonoBehaviour
    {
        #region Variables

        private const string SaveFilePath = "Game/GameData";

        [SerializeField] private GameData _gameData;

        private EventBus _eventBus;

        #endregion

        #region Properties

        public GameData Data => _gameData;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void Bootstrap()
        {
            Load();
            _eventBus.Subscribe<AppStateChangedEvent>(OnAppStateChanged);
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(Data);

            try
            {
                PlayerPrefs.SetString(SaveFilePath, json);
                // Directory.CreateDirectory(GetFilePath());
                // File.WriteAllText(GetFilePath(), json);
            }
            catch (Exception e)
            {
                this.LogError($"Can't save file. {e}");
            }
        }

        #endregion

        #region Private methods

        private string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFilePath);
        }

        private void Load()
        {
            try
            {
                // string json = File.ReadAllText(GetFilePath());
                string json = PlayerPrefs.GetString(SaveFilePath);
                if (string.IsNullOrEmpty(json))
                {
                    _gameData = new GameData();
                    return;
                }
                _gameData = JsonUtility.FromJson<GameData>(json);
            }
            catch
            {
                _gameData = new GameData();
            }
        }

        private void OnAppStateChanged(AppStateChangedEvent args)
        {
            if (!args.HasFocus)
            {
                Save();
            }
        }

        #endregion
    }
}