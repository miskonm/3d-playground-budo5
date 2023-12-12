namespace Playground.Game.Gameplay.Interaction
{
    public abstract class GameInteractionStrategy
    {
        public abstract void FlaskSelected(object flask);
        public abstract void MoveTo(object fromFlask, object toFlask);
    }
}