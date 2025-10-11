using System;

namespace Features.StateMachineModule.Scripts {
    public abstract class StateBase : IState {
        public event Action OnEntered;
        public event Action OnExited;
        
        public void Enter() {
            OnEnteredState();
            OnEntered?.Invoke();
        }

        public void Exit() {
            OnExitedState();
            OnExited?.Invoke();
        }

        protected abstract void OnEnteredState();
        protected abstract void OnExitedState();
    }
}