using Playground.Services.Log;

namespace Playground.Game.Gameplay.Interaction
{
    public class SleepyGameInteractionStrategy : DefaultGameInteractionStrategy
    {
        public override void MoveTo(object fromFlask, object toFlask)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnFlaskSelected(object flask)
        {
            base.OnFlaskSelected(flask);
            
            this.LogError($"OnFlaskSelected fjrifjrfir");
        }
    }
}