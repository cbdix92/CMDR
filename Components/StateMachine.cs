using System;
using System.Linq;
using System.Collections.Generic;

namespace CMDR
{
    public sealed class State
    {
        private StateMachine _parent;
        public bool Set
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
        public object GetData()
        {
            return _possibleStates[CurrentState];
        }
        public void NewState(State state, object data)
        {
            _possibleStates.Add(state, data);
            if(CurrentState == null)
            {
                state.Set = true;
            }
        }
    }
}
