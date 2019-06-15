//---------------------------------------------------------------
using UnityEngine;
using System.Collections;
//---------------------------------------------------------------
public class CharControl : MonoBehaviour 
{
	//---------------------------------------------------------------
	public float Speed = 10f;
	public float RotSpeed = 90f;
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * Speed * Input.GetAxis("Vertical") * Time.deltaTime;
		transform.Rotate(0,RotSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
	}
	//---------------------------------------------------------------
}
//---------------------------------------------------------------