using UnityEngine;
using System.Collections;

public class TransitionDistanceActivation : MonoBehaviour
{
	public TransitionByDistance cameraDistance;

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("ACTIVANDO TRIGGER");
			cameraDistance.InitTransition();
		}
	}
}
