using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bike
{
    public class BikeState_Stop : IBikeState
    {
        protected override void Awake()
        {
            base.Awake();
            Init(State.Stop);
        }

        public override void Handle(MonoStateHandler _handler)
        {
            base.Handle(_handler);
            bikeController.CurrentSpeed = 0;
        }
    }
}
