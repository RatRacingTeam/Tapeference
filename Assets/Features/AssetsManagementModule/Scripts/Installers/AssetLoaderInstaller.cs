using Zenject;

namespace Features.AssetsManagementModule.Scripts.Installers {
    public class AssetLoaderInstaller : Installer<AssetLoaderInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesTo<AddressableAssetLoaderService>()
                .AsSingle();

            Container.BindInterfacesTo<ResourceAssetLoaderService>()
                .AsSingle();

            Container.BindInterfacesTo<AssetLoaderFacadeService>()
                .AsSingle();
        }
    }
}