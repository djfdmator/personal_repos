using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DesignPattern;
using System;

namespace Game.Bike
{
    public class IBikeState : MonoBehaviour, IState
    {
        public enum State
        {
            Start,
            Stop,
            Turn,
        }
        private State state;

        protected BikeController bikeController;

        protected virtual void Awake()
        {

        }

        protected virtual void Init(State thisState)
        {
            state = thisState;
            Activate(false);
        }

        public virtual void Handle(MonoStateHandler _handler)
        {
            bikeController = _handler as BikeController;
        }

        public virtual Enum GetEnum()
        {
            return state;
        }

        public void Activate(bool isOn)
        {
            gameObject.SetActive(isOn);
        }
    }
}