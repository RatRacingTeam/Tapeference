using Features.AssetsManagementModule.Scripts;
using UnityEngine;
using Zenject;

namespace Features.BootstrapModule.Scripts.GlobalInstallers {
    public class ConfigurationsInstaller : Installer<ConfigurationsInstaller> {
        private readonly IAssetLoaderFacadeService _assetLoaderService;

        public ConfigurationsInstaller(IAssetLoaderFacadeService assetLoaderFacadeService) =>
            _assetLoaderService = assetLoaderFacadeService;
        
        public override void InstallBindings() {
            
        }

        private void BindConfiguration<TConfig>(string configAddress) where TConfig : ScriptableObject {
            Container.Bind<TConfig>()
                .FromScriptableObject(_assetLoaderService.LoadAsset<TConfig>(configAddress))
                .AsSingle();
        }
    }
}