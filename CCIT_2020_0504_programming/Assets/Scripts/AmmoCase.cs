using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCase : Item
{
    public int ammoCount;

    private void Awake()
    {
        ammoCount = Random.Range(10, 50);
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);

        other.SendMessage("AddAmmo", ammoCount, SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}
