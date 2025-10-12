using Features.GameFlowModule.Scripts.GameStates;
using Zenject;

namespace Features.GameFlowModule.Scripts.Installers {
    public class GameFlowInstaller : Installer<GameFlowInstaller> {
        public override void InstallBindings() {
            Container.Bind<GameFlowStateMachine>()
                .AsSingle();

            Container.Bind<GameState>()
                .AsSingle();

            Container.BindInterfacesTo<GameFlowInitializationSystem>()
                .AsSingle();
        }
    }
}