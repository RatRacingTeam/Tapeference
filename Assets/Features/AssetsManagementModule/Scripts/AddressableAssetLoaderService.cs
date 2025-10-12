using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Features.AssetsManagementModule.Scripts {
    public class AddressableAssetLoaderService : IAddressableAssetLoaderService {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssets = new();
        
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object {
            if (_cachedAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return cachedAssetHandle.Result as T;
            
            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetName);
            await operationHandle.Task;
            return ProcessOperationResult(assetName, operationHandle);
        }

        public T LoadAsset<T>(string assetName) where T : Object {
            if (_cachedAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return cachedAssetHandle.Result as T;
            
            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetName);
            operationHandle.WaitForCompletion();
            return ProcessOperationResult(assetName, operationHandle);
        }

        public void UnloadAsset(string assetName) {
            if (!_cachedAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return;
            
            cachedAssetHandle.Release();
            _cachedAssets.Remove(assetName);
        }
        
        private T ProcessOperationResult<T>(string assetName, AsyncOperationHandle<T> operationHandle) where T : Object {
            if (operationHandle.Status == AsyncOperationStatus.Failed) {
                string operationException = operationHandle.OperationException.Message;
                operationHandle.Release();
                Assert.IsTrue(false, $"Failed to process operation on asset {assetName} because {operationException}");
            }

            _cachedAssets[assetName] = operationHandle;
            return operationHandle.Result;
        }
    }
}