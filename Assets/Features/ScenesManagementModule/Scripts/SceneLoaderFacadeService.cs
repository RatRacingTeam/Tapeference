using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Global.Scripts.Addressables;

namespace Features.ScenesManagementModule.Scripts {
    public class SceneLoaderFacadeService : ISceneLoaderFacadeService {
        private readonly ISceneLoaderService _addressableSceneLoaderService;
        private readonly ISceneLoaderService _builtInSceneLoaderService;

        public SceneLoaderFacadeService(IAddressableSceneLoaderService addressableSceneLoaderService,
            IBuiltInSceneLoaderService builtInSceneLoaderService) {
            _addressableSceneLoaderService = addressableSceneLoaderService;
            _builtInSceneLoaderService = builtInSceneLoaderService;
        }
        
        public async UniTask LoadSceneAsync(string sceneName, bool unloadRedundant = true) {
            ISceneLoaderService sceneLoaderService = GetSceneLoaderService(sceneName);
            await sceneLoaderService.LoadSceneAsync(sceneName, unloadRedundant);
        }

        public async UniTask UnloadSceneAsync(string sceneName) {
            ISceneLoaderService sceneLoaderService = GetSceneLoaderService(sceneName);
            await sceneLoaderService.UnloadSceneAsync(sceneName);
        }

        public async UniTask UnloadScenesAsync(ICollection<string> scenesNames) {
            ICollection<string> addressableScenesNames = 
                scenesNames.Where(x => AssetAddress.Scenes.AllAddressablesInGroup.Contains(x))
                    .ToList();
            
            ICollection<string> builtInScenesNames = scenesNames.Except(addressableScenesNames)
                .ToList();
            
            await _addressableSceneLoaderService.UnloadScenesAsync(addressableScenesNames);
            await _builtInSceneLoaderService.UnloadScenesAsync(builtInScenesNames);
        }

        private ISceneLoaderService GetSceneLoaderService(string sceneName) =>
            AssetAddress.Scenes.AllAddressablesInGroup.Contains(sceneName)
                ? _addressableSceneLoaderService
                : _builtInSceneLoaderService;
    }
}