using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Features.AssetsManagementModule.Scripts {
    public class ResourceAssetLoaderService : IResourceAssetLoaderService {
        private readonly Dictionary<string, Object> _cachedAssets = new();
        
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object {
            if (_cachedAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return cachedResourceAsset as T;
            
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(assetName);
            await UniTask.WaitUntil(() => resourceRequest.isDone);
            ProcessLoadedResource(assetName, resourceRequest.asset);
            return resourceRequest.asset as T;
        }

        public T LoadAsset<T>(string assetName) where T : Object {
            if (_cachedAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return cachedResourceAsset as T;

            T loadedAsset = Resources.Load<T>(assetName);
            ProcessLoadedResource(assetName, loadedAsset);
            return loadedAsset;
        }

        public void UnloadAsset(string assetName) {
            if (!_cachedAssets.TryGetValue(assetName, out Object cachedResourceAsset))
                return;
            
            Resources.UnloadAsset(cachedResourceAsset);
            _cachedAssets.Remove(assetName);
        }

        private void ProcessLoadedResource(string assetName, Object loadedResource) {
            Assert.IsNotNull(loadedResource, $"Failed to load asset resource {assetName}");
            _cachedAssets[assetName] = loadedResource;
        }
    }
}