using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Features.AssetsManagementModule.Scripts {
    public class ResourcesAssetsModel {
        private readonly Dictionary<string, Object> _cachedResourceAssets = new();

        public IReadOnlyDictionary<string, Object> CachedResourceAssets => 
            _cachedResourceAssets;

        public event Action<string, Object> OnCachedResourceAssetAdded;
        public event Action<string> OnCachedResourceAssetRemoved;
        
        public void AddCachedResourceAsset(string assetName, Object loadedResource) {
            if(_cachedResourceAssets.TryAdd(assetName, loadedResource))
                OnCachedResourceAssetAdded?.Invoke(assetName, loadedResource);
        }

        public void RemoveCachedResourceAsset(string assetName) {
            if(_cachedResourceAssets.Remove(assetName))
                OnCachedResourceAssetRemoved?.Invoke(assetName);
        }
    }
}