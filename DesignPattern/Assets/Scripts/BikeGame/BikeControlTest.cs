using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Bike;

public class BikeControlTest : MonoBehaviour
{
    [SerializeField]
    private BikeController bikeController;

    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            bikeController.StartBike();
        }
        if (GUILayout.Button("Stop"))
        {
            bikeController.StopBike();
        }
        if (GUILayout.Button("Turn Left"))
        {
            bikeController.TurnBike(BikeController.Direction.Left);
        }
        if (GUILayout.Button("Turn Left"))
        {
            bikeController.TurnBike(BikeController.Direction.Left);
        }
    }
}
