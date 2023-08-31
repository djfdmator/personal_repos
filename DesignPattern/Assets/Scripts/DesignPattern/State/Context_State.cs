using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public class Context_State
    {
        protected Dictionary<Enum, IState> stateDictionary = new Dictionary<Enum, IState>();
        public IState currentState;

        protected MonoStateHandler handler;

        public MonoStateHandler GetHandle()
        {
            return handler;
        }
        public void SetHandle(MonoStateHandler _handler)
        {
            handler = _handler;
        }

        public void SetState<T>(IState[] states) where T : Enum
        {
            foreach (var s in states)
            {
                stateDictionary.Add(s.GetEnum(), s);
            }
        }

        public void Transition(Enum state)
        {
            if (currentState != null)
            {
                currentState.Activate(false);
            }

            if (stateDictionary.TryGetValue(state, out currentState))
            {
                currentState.Handle(handler);
                currentState.Activate(true);
                Debug.Log("Transition State :: Current - " + currentState.GetEnum().ToString());
            }
        }
    }
}