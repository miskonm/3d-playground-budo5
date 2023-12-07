using Playground.Services.Gameplay;

namespace Playground.Events
{
    public class GamePreparedEvent
    {
        #region Properties

        public LevelModel LevelModel { get; }

        #endregion

        #region Setup/Teardown

        public GamePreparedEvent(LevelModel levelModel)
        {
            LevelModel = levelModel;
        }

        #endregion
    }
}