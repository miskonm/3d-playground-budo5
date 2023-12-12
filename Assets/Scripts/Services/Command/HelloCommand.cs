using Playground.Services.Log;

namespace Playground.Services.Command
{
    public class HelloCommand : BaseCommand
    {
        #region Public methods

        public override void Execute()
        {
            this.LogError("Hello!");
        }

        #endregion
    }
}