﻿using UnityEngine;
using System.Collections;
//---------------------------------------------------------------
public class CamFollow : MonoBehaviour 
{
	//---------------------------------------------------------------
	//Follow target
	public Transform Target = null;

	//Reference to local transform
	private Transform ThisTransform = null;

	//Linear distance to maintain from target (in world units)
	public float DistanceFromTarget = 10.0f;

	//Height of camera above target
	public float CamHeight = 1f;

	//Damping for rotation
	public float RotationDamp = 4f;

	//Damping for position
	public float PosDamp = 4f;
	//---------------------------------------------------------------
	void Awake()
	{
		//Get transform for camera
		ThisTransform = GetComponent<Transform>();
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void LateUpdate () 
	{
		//Get output velocity
		Vector3 Velocity = Vector3.zero;

		//Calculate rotation interpolate
		ThisTransform.rotation = Quaternion.Slerp(ThisTransform.rotation, Target.rotation, RotationDamp * Time.deltaTime);

		//Get new position
		Vector3 Dest = ThisTransform.position = Vector3.SmoothDamp(ThisTransform.position, Target.position, ref Velocity, PosDamp * Time.deltaTime);

		//Move away from target
		ThisTransform.position = Dest - ThisTransform.forward * DistanceFromTarget;

		//Set height
		ThisTransform.position = new Vector3(ThisTransform.position.x, CamHeight, ThisTransform.position.z);

		//Look at dest
		ThisTransform.LookAt(Dest);
	}
	//---------------------------------------------------------------
}
