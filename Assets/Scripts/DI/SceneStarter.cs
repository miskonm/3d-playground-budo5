using System;
using Playground.Services.Log;
using UnityEngine;

namespace Playground.DI
{
    public class SceneStarter : MonoBehaviour
    {
        private void Awake()
        {
            if (Context.Instance == null)
            {
                Context.Create();
            }
        }

        private void Start()
        {
            Context.Instance.InjectAllSceneObjects();
        }
    }
}