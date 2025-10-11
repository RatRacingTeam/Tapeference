using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.AssetsManagementModule.Scripts {
    public class ResourceAssetLoaderService : IResourceAssetLoaderService {
        private readonly ResourcesAssetsModel _resourcesAssetsModel;

        public ResourceAssetLoaderService(ResourcesAssetsModel resourcesAssetsModel) =>
            _resourcesAssetsModel = resourcesAssetsModel;
        
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object {
            if (_resourcesAssetsModel.CachedResourceAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return cachedResourceAsset as T;
            
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(assetName);
            await UniTask.WaitUntil(() => resourceRequest.isDone);
            CacheResourceAssetIfValid(assetName, resourceRequest.asset);
            return resourceRequest.asset as T;
        }

        public T LoadAsset<T>(string assetName) where T : Object {
            if (_resourcesAssetsModel.CachedResourceAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return cachedResourceAsset as T;

            T loadedAsset = Resources.Load<T>(assetName);
            CacheResourceAssetIfValid(assetName, loadedAsset);
            return loadedAsset;
        }

        public void UnloadAsset(string assetName) {
            if (!_resourcesAssetsModel.CachedResourceAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return;
            
            Resources.UnloadAsset(cachedResourceAsset);
            _resourcesAssetsModel.RemoveCachedResourceAsset(assetName);
        }

        private void CacheResourceAssetIfValid(string assetName, Object loadedResource) {
            if(loadedResource is not null) 
                _resourcesAssetsModel.AddCachedResourceAsset(assetName, loadedResource);
        }
    }
}