using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private bool west = false;
    private bool east = false;
    private bool north = false;
    private bool south = false;

    private float posX;
    private float posY;

    void LateUpdate()
    {
        float prePosX = posX;
        float prePosY = posY;
        posX = player.localPosition.x;
        posY = player.localPosition.y;

        if (west == true && (prePosX - posX) > 0.0f)
        {
            posX = prePosX;
        }
        if (east == true && (prePosX - posX) < 0.0f)
        {
            posX = prePosX;
        }
        if (north == true && (prePosY - posY) < 0.0f)
        {
            posY = prePosY;
        }
        if (south == true && (prePosY - posY) > 0.0f)
        {
            posY = prePosY;
        }

        transform.localPosition = new Vector3(posX, posY, transform.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "west")
        {
            west = true;
        }
        if (collision.tag == "east")
        {
            east = true;
        }
        if (collision.tag == "north")
        {
            north = true;
        }
        if (collision.tag == "south")
        {
            south = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "west")
        {
            west = false;
        }
        if (collision.tag == "east")
        {
            east = false;
        }
        if (collision.tag == "north")
        {
            north = false;
        }
        if (collision.tag == "south")
        {
            south = false;
        }
    }
    ////---------------------------------------------------------------
    ////Follow target
    //public Transform Target = null;

    ////Reference to local transform
    //private Transform ThisTransform = null;

    ////Linear distance to maintain from target (in world units)
    //public float DistanceFromTarget = 10.0f;

    ////Height of camera above target
    //public float CamHeight = 1f;

    ////Damping for rotation
    //public float RotationDamp = 4f;

    ////Damping for position
    //public float PosDamp = 4f;
    ////---------------------------------------------------------------
    //void Awake()
    //{
    //    //Get transform for camera
    //    ThisTransform = GetComponent<Transform>();
    //}
    ////---------------------------------------------------------------
    //// Update is called once per frame
    //void LateUpdate()
    //{
    //    //Get output velocity
    //    Vector3 Velocity = Vector3.zero;

    //    //Calculate rotation interpolate
    //    ThisTransform.rotation = Quaternion.Slerp(ThisTransform.rotation, Target.rotation, RotationDamp * Time.deltaTime);

    //    //Get new position
    //    Vector3 Dest = ThisTransform.position = Vector3.SmoothDamp(ThisTransform.position, Target.position, ref Velocity, PosDamp * Time.deltaTime);

    //    //Move away from target
    //    ThisTransform.position = Dest - ThisTransform.forward * DistanceFromTarget;

    //    //Set height
    //    ThisTransform.position = new Vector3(ThisTransform.position.x, CamHeight, ThisTransform.position.z);

    //    //Look at dest
    //    ThisTransform.LookAt(Dest);
    //}
    ////----
}
