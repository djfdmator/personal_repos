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
}
