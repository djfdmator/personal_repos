using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bike
{
    public class BikeState_Start : IBikeState
    {
        protected override void Awake()
        {
            base.Awake();
            Init(State.Start);
        }

        public override void Handle(MonoStateHandler _handler)
        {
            base.Handle(_handler);
            bikeController.CurrentSpeed = bikeController.maxSpeed;
        }

        public void Update()
        {
            if (bikeController == null)
                return;

            bikeController.transform.Translate(Vector3.forward * (bikeController.CurrentSpeed * Time.deltaTime));
        }
    }
}