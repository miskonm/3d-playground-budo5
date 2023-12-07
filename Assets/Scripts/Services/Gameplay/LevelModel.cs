using System;
using System.Collections.Generic;

namespace Playground.Services.Gameplay
{
    [Serializable]
    public class LevelModel
    {
        #region Properties

        public string Id { get; }
        public LevelState LevelState { get; private set; }
        // public List<UnitModel> Blockes { get; }

        #endregion

        #region Setup/Teardown

        public LevelModel(string id)
        {
            Id = id;
            LevelState = LevelState.Preparing;
        }

        #endregion

        #region Public methods

        public void Dispose() { }

        public void SetLevelState(LevelState value)
        {
            LevelState = value;
        }

        #endregion
    }
}