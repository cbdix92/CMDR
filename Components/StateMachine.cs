using System;
using System.Linq;
using System.Collections.Generic;

namespace CMDR.Components
{
    public sealed class State
    {
        private StateMachine _parent;
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
        public State(StateMachine parent)
        {
            _parent = parent;
        }
    }
    public class StateMachine:Component
    {
        public State CurrentState { get; internal set; }
        private Dictionary<State, object> _possibleStates;

        public StateMachine():base(ComponentType.StateMachine)
        {
            _possibleStates = new Dictionary<State, object>();
        }
        public object GetData(State state)
        {
            return _possibleStates[state];
        }
        public void NewState(object data)
        {
            _possibleStates.Add(new State(this), data);
        }
    }
}
