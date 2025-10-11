using Features.GameFlowModule.Scripts.GameStates;
using Zenject;

namespace Features.GameFlowModule.Scripts {
    public class GameFlowInitializationSystem : IInitializable {
        private readonly GameFlowStateMachine _gameFlowStateMachine;
        private readonly GameState _gameState;

        public GameFlowInitializationSystem(GameFlowStateMachine gameFlowStateMachine, GameState gameState) {
            _gameFlowStateMachine = gameFlowStateMachine;
            _gameState = gameState;
        }
        public void Initialize() =>
            InitializeGameFlowStateMachine();

        private void InitializeGameFlowStateMachine() {
            GameFlowState[] gameFlowStates = {
                _gameState
            };
            
            _gameFlowStateMachine.SetStates(gameFlowStates);
        }
    }
}