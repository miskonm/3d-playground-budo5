using System;
using Playground.Services.Log;
using UnityEngine;

namespace Playground.DI
{
    public class SceneStarter : MonoBehaviour
    {
        private void Awake()
        {
            this.LogError("Awake");
            if (Context.Instance == null)
            {
                Context.Create();
            }
        }

        private void Start()
        {
            this.LogError("Start");
            Context.Instance.InjectAllSceneObjects();
        }
    }
}