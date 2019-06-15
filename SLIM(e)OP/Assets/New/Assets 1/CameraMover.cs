//-----------------------------
using UnityEngine;
using System.Collections;
//-----------------------------
public class CameraMover : MonoBehaviour 
{
	//-----------------------------
	//Total time for animation
	public float TotalTime = 5.0f;

	//Total Distance to move on each axis
	public float TotalDistance = 30.0f;

	//Curves for motion
	public AnimationCurve XCurve;
	public AnimationCurve YCurve;
	public AnimationCurve ZCurve;

	//Transform for this object
	private Transform ThisTransform = null;
	//-----------------------------
	void Start()
	{
		//Get transform component
		ThisTransform = GetComponent<Transform>();

		//Start animation
		StartCoroutine(PlayAnim());
	}
	//-----------------------------
	public IEnumerator PlayAnim()
	{
		//Time that has passed since anim start
		float TimeElapsed = 0.0f;

		while(TimeElapsed < TotalTime)
		{
			//Get normalized time
			float NormalTime = TimeElapsed / TotalTime;

			//Sample graph for X Y and Z
			Vector3 NewPos = ThisTransform.right.normalized * XCurve.Evaluate(NormalTime) * TotalDistance;
			NewPos += ThisTransform.up.normalized * YCurve.Evaluate(NormalTime) * TotalDistance;
			NewPos += ThisTransform.forward.normalized * ZCurve.Evaluate(NormalTime) * TotalDistance;

			//Update position
			ThisTransform.position = NewPos;

			//Wait until next frame
			yield return null;

			//Update time
			TimeElapsed += Time.deltaTime;
		}
	}
	//-----------------------------

}
//-----------------------------