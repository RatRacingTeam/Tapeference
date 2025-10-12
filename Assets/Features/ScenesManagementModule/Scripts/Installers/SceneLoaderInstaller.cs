using Zenject;

namespace Features.ScenesManagementModule.Scripts.Installers {
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesTo<AddressableSceneLoaderService>()
                .AsSingle();
            
            Container.BindInterfacesTo<BuiltInSceneLoaderService>()
                .AsSingle();
            
            Container.BindInterfacesTo<SceneLoaderFacadeService>()
                .AsSingle();
        }
    }
}