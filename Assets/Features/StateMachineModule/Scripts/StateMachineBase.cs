using System;
using System.Collections.Generic;

namespace Features.StateMachineModule.Scripts {
    public abstract class StateMachineBase<TContext> where TContext : StateBase {
        private readonly Dictionary<Type, TContext> _processedStates = new();
        private TContext _currentState;
        
        public TContext CurrentState => _currentState;
        
        public void SetStates(IEnumerable<TContext> states) {
            _processedStates.Clear();
            
            foreach (TContext state in states) 
                _processedStates[state.GetType()] = state;
        }

        public void EnterState<TInput>() where TInput : StateBase {
            _currentState?.Exit();

            if (!_processedStates.TryGetValue(typeof(TInput), out _currentState))
                throw new Exception($"Unable to process state because it is not present in processed states {typeof(TInput)}");
            
            _currentState.Enter();
        }
    }
}