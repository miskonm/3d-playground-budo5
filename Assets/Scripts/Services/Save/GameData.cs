using System;
using System.Collections.Generic;

namespace Playground.Services.Save
{
    [Serializable]
    public class GameData
    {
        #region Variables

        public UserSaveData User = new();
        public AnalyticsSaveData Analytics = new();
        public OnboardingSaveData Onboarding = new();
        public ShopSaveData Shop = new();

        #endregion
    }

    // --------------- For example ----------------------

    [Serializable]
    public class UserSaveData
    {
        #region Variables

        public int LevelIndex;
        public List<LevelSaveData> Levels = new();

        #endregion
    }

    [Serializable]
    public class LevelSaveData
    {
        #region Variables

        public float BestTimeInSeconds;
        public int HighScore;

        #endregion
    }

    [Serializable]
    public class OnboardingSaveData
    {
        #region Variables

        public bool IsBoughtFreeItemInShop;
        public List<string> ShowedOnboardings = new();

        #endregion
    }

    [Serializable]
    public class ShopSaveData
    {
        #region Variables

        public List<string> BoughtBackground = new();
        public List<string> BoughtBackground1 = new();
        public List<string> BoughtBackground2 = new();

        #endregion
    }

    [Serializable]
    public class AnalyticsSaveData { }
}