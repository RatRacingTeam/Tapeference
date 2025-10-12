using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Features.ScenesManagementModule.Scripts {
    public interface ISceneLoaderService {
        public UniTask LoadSceneAsync(string sceneName, bool unloadRedundant = true);
        public UniTask UnloadSceneAsync(string sceneName);
        public UniTask UnloadScenesAsync(ICollection<string> scenesNames);
    }
}