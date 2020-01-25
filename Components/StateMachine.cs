using System;
using System.Linq;
using System.Collections.Generic;

namespace CMDR.Components
{
    public sealed class State
    {
        private StateMachine<object> _parent;
        public bool SubState
        {
            set
            {
                if (value)
                {
                    _parent.CurrentState = this;
                }
            }
        }
        public State(StateMachine<object> parent)
        {
            _parent = parent;
        }
    }
    public class StateMachine<T>:Component
    {
        public State CurrentState { get; internal set; }
        private Dictionary<State, T> _possibleStates;

        public StateMachine():base(ComponentType.StateMachine)
        {
            _possibleStates = new Dictionary<State, T>();
        }
    }
}
