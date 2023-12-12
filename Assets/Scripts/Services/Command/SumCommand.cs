using Playground.Services.Log;

namespace Playground.Services.Command
{
    public class SumCommand : BaseCommand
    {
        #region Variables

        private readonly int _a;
        private readonly int _b;

        #endregion

        #region Setup/Teardown

        public SumCommand(int a, int b)
        {
            _a = a;
            _b = b;
        }

        #endregion

        #region Public methods

        public override void Execute()
        {
            this.LogError($"Sum of {_a} + {_b} = {_a + _b}!");
        }

        #endregion
    }
}