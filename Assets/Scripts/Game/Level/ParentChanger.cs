using System.Collections.Generic;
using UnityEngine;

namespace Playground.Game.Level
{
    public class ParentChanger : MonoBehaviour
    {
        #region Variables

        private readonly Dictionary<Collider, Transform> _baseTransformByColliders = new();

        #endregion

        #region Unity lifecycle

        private void OnTriggerEnter(Collider other)
        {
            _baseTransformByColliders.Add(other, other.transform.parent);
            other.transform.SetParent(transform);
        }

        private void OnTriggerExit(Collider other)
        {
            Transform parent = _baseTransformByColliders[other];
            _baseTransformByColliders.Remove(other);
            other.transform.SetParent(parent);
        }

        #endregion
    }
}