using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Features.AssetsManagementModule.Scripts {
    public class AddressableAssetLoaderService : IAddressableAssetLoaderService {
        private readonly AddressablesAssetsModel _addressablesAssetsModel;

        public AddressableAssetLoaderService(AddressablesAssetsModel addressablesAssetsModel) =>
            _addressablesAssetsModel = addressablesAssetsModel;
        
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object {
            if (_addressablesAssetsModel.CachedAddressableAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return cachedAssetHandle.Result as T;
            
            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetName);
            await operationHandle.Task;
            return ProcessOperationResult(assetName, operationHandle);
        }

        public T LoadAsset<T>(string assetName) where T : Object {
            if (_addressablesAssetsModel.CachedAddressableAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return cachedAssetHandle.Result as T;
            
            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetName);
            operationHandle.WaitForCompletion();
            return ProcessOperationResult(assetName, operationHandle);
        }

        public void UnloadAsset(string assetName) {
            if (!_addressablesAssetsModel.CachedAddressableAssets.TryGetValue(assetName, out AsyncOperationHandle cachedAssetHandle))
                return;
            
            cachedAssetHandle.Release();
            _addressablesAssetsModel.RemoveCachedAddressableAsset(assetName);
        }
        
        private T ProcessOperationResult<T>(string assetName, AsyncOperationHandle<T> operationHandle) where T : Object {
            if (operationHandle.Status != AsyncOperationStatus.Succeeded) {
                operationHandle.Release();
                return null;
            }
            
            _addressablesAssetsModel.AddCachedAddressableAsset(assetName, operationHandle);
            return operationHandle.Result;
        }
    }
}