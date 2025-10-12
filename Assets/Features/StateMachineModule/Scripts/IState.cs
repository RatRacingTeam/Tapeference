using System;

namespace Features.StateMachineModule.Scripts {
    public interface IState {
        public event Action OnEntered;
        public event Action OnExited;
        public void Enter();
        public void Exit();
    }
}