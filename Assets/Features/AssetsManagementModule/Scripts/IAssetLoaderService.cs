using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.AssetsManagementModule.Scripts {
    public interface IAssetLoaderService {
        public UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object;
        public T LoadAsset<T>(string assetName) where T : Object;
        public void UnloadAsset(string assetName);
    }
}