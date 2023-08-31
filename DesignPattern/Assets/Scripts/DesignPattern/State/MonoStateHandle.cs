using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public class MonoStateHandler : MonoBehaviour
    {
        protected Context_State stateContext;

        protected virtual void InitStateHandler<T>(IState[] states, Enum initState) where T : Enum
        {
            stateContext = new Context_State();
            stateContext.SetHandle(this);
            stateContext.SetState<T>(states);
            stateContext.Transition(initState);
        }
    }
}