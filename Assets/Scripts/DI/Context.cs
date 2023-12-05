using System;
using UnityEngine;

namespace Playground.DI
{
    public class Context : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Installer _installer;

        private Container _container;

        #endregion

        #region Properties

        public static Context Instance { get; private set; }

        #endregion

        #region Public methods

        public static void Create()
        {
            if (Instance != null)
            {
                Debug.LogError("Try create context when already existed!");
                return;
            }

            Context prefab = Resources.Load<Context>(nameof(Context));
            if (prefab == null)
            {
                throw new NullReferenceException($"There is no prefab '{nameof(Context)}' in Resources!");
            }

            Instance = Instantiate(prefab);
            DontDestroyOnLoad(Instance.gameObject);
            Instance.Initialize();
        }

        public void InjectAllSceneObjects()
        {
            _container.InjectAllSceneObjects();
        }

        #endregion

        #region Private methods

        private void Initialize()
        {
            if (_installer == null)
            {
                throw new NullReferenceException("No installer set!");
            }

            _container = new Container(transform);
            _installer.Install(_container);
            _container.InstantiateNonLazy();
        }

        #endregion
    }
}