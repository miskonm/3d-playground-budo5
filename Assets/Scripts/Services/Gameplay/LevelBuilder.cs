using Playground.Game.Gameplay.Interaction;

namespace Playground.Services.Gameplay
{
    public class LevelBuilder
    {
        #region Public methods

        public LevelModel Build(LevelData levelData)
        {
            // Create prefabs for level
            return new LevelModel(levelData.id, LevelType.Default);
        }

        #endregion
    }
}