using Cysharp.Threading.Tasks;
using Global.Scripts.Addressables;
using UnityEngine;

namespace Features.AssetsManagementModule.Scripts {
    public class AssetLoaderFacadeService : IAssetLoaderFacadeService {
        private readonly IAssetLoaderService _addressableAssetLoaderService;
        private readonly IAssetLoaderService _resourceAssetLoaderService;

        public AssetLoaderFacadeService(IAddressableAssetLoaderService addressableAssetLoaderService,
            IResourceAssetLoaderService resourceAssetLoaderService) {
            _addressableAssetLoaderService = addressableAssetLoaderService;
            _resourceAssetLoaderService = resourceAssetLoaderService;
        }
        
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object {
            IAssetLoaderService assetLoaderService = GetAssetLoaderService(assetName);
            return await assetLoaderService.LoadAssetAsync<T>(assetName);
        }

        public T LoadAsset<T>(string assetName) where T : Object {
            IAssetLoaderService assetLoaderService = GetAssetLoaderService(assetName);
            return assetLoaderService.LoadAsset<T>(assetName);
        }

        public void UnloadAsset(string assetName) {
            IAssetLoaderService assetLoaderService = GetAssetLoaderService(assetName);
            assetLoaderService.UnloadAsset(assetName);
        }

        private IAssetLoaderService GetAssetLoaderService(string assetName) =>
            AssetAddress.AllAddressablesInGroups.Contains(assetName)
                ? _addressableAssetLoaderService
                : _resourceAssetLoaderService;
    }
}