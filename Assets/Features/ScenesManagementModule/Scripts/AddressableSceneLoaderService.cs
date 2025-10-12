using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Features.ScenesManagementModule.Scripts {
    public class AddressableSceneLoaderService : IAddressableSceneLoaderService {
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _loadedScenes = new();
        
        public async UniTask LoadSceneAsync(string sceneName, bool unloadRedundant = true) {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            if (unloadRedundant)
                await UnloadScenesAsync(_loadedScenes.Keys);
            
            AsyncOperationHandle<SceneInstance> sceneOperationHandle = Addressables.LoadSceneAsync(sceneName, GetSceneLoadMode(unloadRedundant));
            await sceneOperationHandle.Task;
            if (sceneOperationHandle.Status == AsyncOperationStatus.Failed) {
                string operationException = sceneOperationHandle.OperationException.Message; 
                sceneOperationHandle.Release();
                Assert.IsFalse(true, $"Unable to load scene {sceneName} because {operationException}");
            }
            _loadedScenes[sceneName] = sceneOperationHandle;
        }

        public async UniTask UnloadSceneAsync(string sceneName) {
            if (!_loadedScenes.TryGetValue(sceneName, out AsyncOperationHandle<SceneInstance> sceneOperationHandle))
                return;

            AsyncOperationHandle<SceneInstance> unloadSceneOperationHandle = Addressables.UnloadSceneAsync(sceneOperationHandle);
            await unloadSceneOperationHandle.Task;
            string operationException = unloadSceneOperationHandle.OperationException.Message;
            unloadSceneOperationHandle.Release();
            Assert.IsFalse(unloadSceneOperationHandle.Status == AsyncOperationStatus.Failed, 
                $"Unable to unload scene {sceneName} because {operationException}");
            _loadedScenes.Remove(sceneName);
        }

        public async UniTask UnloadScenesAsync(ICollection<string> scenesNames) {
            foreach (string sceneName in scenesNames)
                await UnloadSceneAsync(sceneName);
        }

        private LoadSceneMode GetSceneLoadMode(bool unloadRedundant) =>
            unloadRedundant ? LoadSceneMode.Single : LoadSceneMode.Additive;
    }
}