namespace Playground.Events
{
    public class GameFinishedEvent
    {
        #region Properties

        public bool IsFinished { get; }

        #endregion

        #region Setup/Teardown

        public GameFinishedEvent(bool isFinished)
        {
            IsFinished = isFinished;
        }

        #endregion
    }
}