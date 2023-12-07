using System;
using System.Collections.Generic;
using UnityEngine;

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
    public class UserSaveData : ILevelIncrementable
    {
        #region Variables

        [SerializeField]private int _levelIndex;
        public List<LevelSaveData> Levels = new();

        public int LevelIndex => _levelIndex;

        #endregion

        void ILevelIncrementable.IncrementLevelIndex()
        {
            _levelIndex++;
        }
    }

    public interface ILevelIncrementable
    {
        void IncrementLevelIndex();
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