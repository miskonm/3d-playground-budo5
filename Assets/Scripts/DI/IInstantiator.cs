using UnityEngine;

namespace Playground.DI
{
    public interface IInstantiator
    {
        #region Public methods

        TComponent InstantiatePrefab<TComponent>(GameObject prefab, Transform transform)
            where TComponent : MonoBehaviour;

        TComponent InstantiatePrefab<TComponent>(Component prefab, Transform transform)
            where TComponent : MonoBehaviour;

        #endregion
    }
}