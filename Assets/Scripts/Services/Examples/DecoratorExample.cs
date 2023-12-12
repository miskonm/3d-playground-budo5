using System.Collections.Generic;
using System.Linq;
using Playground.Services.Gameplay;
using UnityEngine;

namespace Playground.Services.Examples
{
    public class DecoratorExample
    {
        #region Public Nested Types

        public class Programm
        {
            #region Variables

            private LevelLoader _levelLoader;

            #endregion

            #region Public methods

            public void Init()
            {
                BuildInLevelLoader buildInLevelLoader = new BuildInLevelLoader();
                // ConfigLevelLoader configLevelLoader = new ConfigLevelLoader(buildInLevelLoader);
                _levelLoader = new ServerLevelLoader(buildInLevelLoader);
            }

            public LevelData LoadLevel()
            {
                int levelIndex = 4;
                return _levelLoader.LoadLevel(levelIndex);
            }

            #endregion
        }

        public abstract class LevelLoader
        {
            #region Public methods

            public abstract LevelData LoadLevel(int levelIndex);

            #endregion
        }

        public class BuildInLevelLoader : LevelLoader
        {
            #region Public methods

            public override LevelData LoadLevel(int levelIndex)
            {
                TextAsset textAsset = Resources.Load<TextAsset>($"Configs/Level/Level_{levelIndex}");
                return JsonUtility.FromJson<LevelData>(textAsset.text);
            }

            #endregion
        }

        public class ConfigLevelLoader : LevelLoader
        {
            #region Variables

            private readonly LevelLoader _loader;
            
            private Config _config;

            #endregion

            #region Setup/Teardown

            public ConfigLevelLoader(LevelLoader loader)
            {
                _loader = loader;
            }

            #endregion

            #region Public methods

            public override LevelData LoadLevel(int levelIndex)
            {
                LevelData levelData = _config.Levels.FirstOrDefault(x => x.id == $"Level_{levelIndex}");
                if (levelData != null)
                {
                    return levelData;
                }

                return _loader.LoadLevel(levelIndex);
            }

            #endregion

            #region Local data

            private class Config : ScriptableObject
            {
                #region Variables

                public List<LevelData> Levels;

                #endregion
            }

            #endregion
        }
        
        public class ServerLevelLoader : LevelLoader
        {
            #region Variables

            private readonly LevelLoader _loader;

            #endregion

            #region Setup/Teardown

            public ServerLevelLoader(LevelLoader loader)
            {
                _loader = loader;
            }

            #endregion

            #region Public methods

            public override LevelData LoadLevel(int levelIndex)
            {
                LevelData levelData = TryLoadFromServer(levelIndex);
                if (levelData != null)
                {
                    return levelData;
                }

                return _loader.LoadLevel(levelIndex);
            }

            private LevelData TryLoadFromServer(int levelIndex)
            {
                return null;
            }

            #endregion
            
        }

        #endregion
    }
}