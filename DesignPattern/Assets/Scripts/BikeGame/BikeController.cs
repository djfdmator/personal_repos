using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DesignPattern;

using State = Game.Bike.IBikeState.State;

namespace Game.Bike
{
    public class BikeController : MonoStateHandler
    {
        public enum Direction
        {
            Left = -1,
            Right = 1,
        }

        public float maxSpeed = 2.0f;
        public float turnDistance = 2.0f;

        public float CurrentSpeed { get; set; }

        public Direction CurrentTurnDirection { get; private set; }

        private void Start()
        {
            InitStateHandler<State>(transform.GetComponentsInChildren<IState>(true), State.Stop);
        }

        public void StartBike()
        {
            stateContext.Transition(State.Start);
        }

        public void StopBike()
        {
            stateContext.Transition(State.Stop);
        }

        public void TurnBike(Direction direction)
        {
            CurrentTurnDirection = direction;
            stateContext.Transition(State.Turn);
        }
    }
}