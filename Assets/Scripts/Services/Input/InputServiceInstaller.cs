

using Playground.DI;

namespace Playground.Services.Input
{
    public class InputServiceInstaller : Installer<InputServiceInstaller>
    {
        #region Public methods

        protected override void InstallBindings()
        {
#if UNITY_STANDALONE
            Container.Bind<IInputService>().To<StandaloneInputService>();
#else
            Debug.LogError($"Not supported platform. Use default input service");
            Container.Bind<IInputService>().To<StandaloneInputService>();
#endif
        }

        #endregion
    }
}