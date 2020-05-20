//--------------------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------------------
public class HealthRestore : Item
{
	//--------------------------------------------------
	//Health Points for this restore
	public float HealthPoints = 50f;

	//--------------------------------------------------
	public override void OnTriggerStay(Collider other)
	{
        base.OnTriggerStay(other);

		other.SendMessage("ChangeHealth", HealthPoints, SendMessageOptions.DontRequireReceiver);

		//Destroy this object
		Destroy(gameObject);
	}
	//--------------------------------------------------
}
//--------------------------------------------------