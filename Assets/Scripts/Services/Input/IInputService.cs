using UnityEngine;

namespace Playground.Services.Input
{
    public interface IInputService
    {
        #region Properties

        Vector2 Axes { get; }
        bool IsJump { get; }

        #endregion
    }
}