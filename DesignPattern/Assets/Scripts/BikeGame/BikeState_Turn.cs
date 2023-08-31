using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bike
{
    public class BikeState_Turn : IBikeState
    {
        [SerializeField]
        private Vector3 turnDirection;

        protected override void Awake()
        {
            base.Awake();
            Init(State.Turn);
        }

        public override void Handle(MonoStateHandler _handler)
        {
            base.Handle(_handler);

            turnDirection.x = (float)bikeController.CurrentTurnDirection;
        }
    }
}
