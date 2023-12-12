using Playground.Services.Log;

namespace Playground.Services.Command
{
    public class SaySmthCommand : BaseCommand
    {
        #region Variables

        private readonly string _message;

        #endregion

        #region Setup/Teardown

        public SaySmthCommand(string message)
        {
            _message = message;
        }

        #endregion

        #region Public methods

        public override void Execute()
        {
            this.LogError($"I'm saying '{_message}'!");
        }

        #endregion
    }
}