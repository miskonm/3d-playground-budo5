using Playground.Services.Gameplay;

namespace Playground.Events
{
    public class GameStartedEvent
    {
        #region Properties

        public LevelModel LevelModel { get; }

        #endregion

        #region Setup/Teardown

        public GameStartedEvent(LevelModel levelModel)
        {
            LevelModel = levelModel;
        }

        #endregion
    }
}