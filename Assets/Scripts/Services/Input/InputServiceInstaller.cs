using Zenject;

namespace Playground.Services.Input
{
    public class InputServiceInstaller : Installer<InputServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
#if UNITY_STANDALONE
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
#else
            Debug.LogError($"Not supported platform. Use default input service");
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
#endif
        }

        #endregion
    }
}