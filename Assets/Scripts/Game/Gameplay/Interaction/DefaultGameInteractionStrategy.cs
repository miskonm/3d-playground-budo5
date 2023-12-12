using Playground.Services.Log;

namespace Playground.Game.Gameplay.Interaction
{
    public class DefaultGameInteractionStrategy : GameInteractionStrategy
    {
        public override void FlaskSelected(object flask)
        {
            this.LogError($"FlaskSelected olol");


            OnFlaskSelected(flask);
        }

        public override void MoveTo(object fromFlask, object toFlask)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual void OnFlaskSelected(object flask){}
    }
}