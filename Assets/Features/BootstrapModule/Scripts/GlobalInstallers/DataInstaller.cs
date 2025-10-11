using Features.AssetsManagementModule.Scripts;
using Zenject;

namespace Features.BootstrapModule.Scripts.GlobalInstallers {
    public class DataInstaller : Installer<DataInstaller> {
        public override void InstallBindings() {
            Container.Bind<AddressablesAssetsModel>()
                .AsSingle();

            Container.Bind<ResourcesAssetsModel>()
                .AsSingle();
        }
    }
}