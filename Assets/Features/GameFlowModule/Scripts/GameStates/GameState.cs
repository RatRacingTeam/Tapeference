using Features.ScenesManagementModule.Scripts;
using Global.Scripts.Addressables;

namespace Features.GameFlowModule.Scripts.GameStates {
    public class GameState : GameFlowState {
        private readonly ISceneLoaderFacadeService _sceneLoaderService;

        public GameState(ISceneLoaderFacadeService sceneLoaderService) {
            _sceneLoaderService = sceneLoaderService;
        } 
        
        protected override void OnEnteredState() =>
            _sceneLoaderService.LoadSceneAsync(AssetAddress.Scenes.GameScene);

        protected override void OnExitedState() { }
    }
}