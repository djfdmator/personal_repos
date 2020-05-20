using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Distance for collection
    public float DistanceCollection = 2f;

    //--------------------------------------------------
    public virtual void OnTriggerStay(Collider other)
    {
        //Check if colliding object is near enough to collect this bonus
        if (Vector3.Distance(other.transform.position, transform.position) > DistanceCollection) return;
        if (other.CompareTag("Player")) return;
    }
    //--------------------------------------------------
}
