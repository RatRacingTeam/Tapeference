using Features.GameFlowModule.Scripts;
using Features.GameFlowModule.Scripts.GameStates;
using UnityEngine;
using Zenject;

namespace Features.BootstrapModule.Scripts {
    public class ProjectBootstrap : MonoBehaviour {
        private GameFlowStateMachine _gameFlowStateMachine;

        [Inject]
        public void InjectDependencies(GameFlowStateMachine gameFlowStateMachine) =>
            _gameFlowStateMachine = gameFlowStateMachine;

        private void Start() =>
            _gameFlowStateMachine.EnterState<GameState>();
    }
}