

using Playground.DI;
using UnityEngine;

namespace Playground.Services.UI
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        #region Variables

        private const string UILayerProviderPath = "UI/UIService";

        #endregion

        #region Public methods

        protected override void InstallBindings()
        {
            Container.Bind<UIService>();
            Container.Bind<UIScreenProvider>();
            
            // TODO: Make more usefull
            GameObject prefab = Resources.Load<GameObject>(UILayerProviderPath);
            Container.Bind<UILayerProvider>().FromPrefab(prefab);

        }

        #endregion
    }
}