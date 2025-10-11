using UnityEngine;

namespace Features.GameFlowModule.Scripts.GameStates {
    public class GameState : GameFlowState {
        protected override void OnEnteredState() {
            Debug.LogError("Entered game state");
        }

        protected override void OnExitedState() { }
    }
}