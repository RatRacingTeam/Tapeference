using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Features.ScenesManagementModule.Scripts {
    public class BuiltInSceneLoaderService : IBuiltInSceneLoaderService {
        private readonly Dictionary<string, Scene> _loadedScenes = new();
        
        public async UniTask LoadSceneAsync(string sceneName, bool unloadRedundant = true) {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            if (unloadRedundant)
                await UnloadScenesAsync(_loadedScenes.Keys);

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, GetSceneLoadMode(unloadRedundant));
            if(loadSceneOperation != null)
                await UniTask.WaitUntil(() => loadSceneOperation.isDone);

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            Assert.IsTrue(loadSceneOperation != null && loadedScene.isLoaded, $"Failed to load scene {sceneName}");
            _loadedScenes[sceneName] = loadedScene;
        }

        public async UniTask UnloadSceneAsync(string sceneName) {
            if (!_loadedScenes.TryGetValue(sceneName, out Scene loadedScene))
                return;

            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(loadedScene);
            if(unloadSceneOperation != null)
                await UniTask.WaitUntil(() => unloadSceneOperation.isDone);
            
            Assert.IsTrue(unloadSceneOperation != null && !loadedScene.isLoaded, $"Failed to unload scene {sceneName}");
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