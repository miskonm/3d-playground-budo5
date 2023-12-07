using Playground.Services.GameManagementNew.StateMachine;

namespace Playground.Services.GameManagementNew
{
    // public class GameManagementServiceInstaller : Installer<GameManagementServiceInstaller>
    // {
    //     public override void InstallBindings()
    //     {
    //         Container
    //             .Bind<GameManagementService>()
    //             .FromSubContainerResolve()
    //             .ByMethod(InstallService)
    //             .AsSingle();
    //     }
    //
    //     private void InstallService(DiContainer subContainer)
    //     {
    //         subContainer.Bind<GameManagementService>().AsSingle();
    //         subContainer.Bind<GameStateMachine>().AsSingle();
    //         subContainer.Bind<GameStateFactory>().AsSingle();
    //     }
    // }
}