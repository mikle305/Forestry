using System;
using System.Collections.Generic;
using Additional;
using GameFlow.Stages;

namespace GameFlow.Context
{
    public class GameStateMachine
    {
        private GameState _currentState;
        private Dictionary<Type, GameState> _states;


        public void Enter<T>() where T : GameState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void AddState(GameState state)
        {
            Type type = state.GetType();
            if (_states.ContainsKey(type))
                ThrowUtils.StateDuplicated();
            else
                _states[type] = state;
        }
    }
}