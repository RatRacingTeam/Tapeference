using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Features.AssetsManagementModule.Scripts {
    public class AddressablesAssetsModel {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAddressableAssets = new();

        public IReadOnlyDictionary<string, AsyncOperationHandle> CachedAddressableAssets => 
            _cachedAddressableAssets;

        public event Action<string, AsyncOperationHandle> OnCachedAddressableAssetAdded;
        public event Action<string> OnCachedAddressableAssetRemoved;
        
        public void AddCachedAddressableAsset(string assetAddress, AsyncOperationHandle operationHandle) {
            if (_cachedAddressableAssets.TryAdd(assetAddress, operationHandle))
                OnCachedAddressableAssetAdded?.Invoke(assetAddress, operationHandle);
        }

        public void RemoveCachedAddressableAsset(string assetAddress) {
            if(_cachedAddressableAssets.Remove(assetAddress))
                OnCachedAddressableAssetRemoved?.Invoke(assetAddress);
        }
    }
}