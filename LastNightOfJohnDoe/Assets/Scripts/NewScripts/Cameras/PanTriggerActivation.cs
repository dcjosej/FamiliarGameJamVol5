using UnityEngine;
using System.Collections;

public class PanTriggerActivation : MonoBehaviour {

	public CameraController cameraController;

	
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("ACTIVANDO TRIGGER");

			if(other.transform.position.z > transform.position.z)
			{
				cameraController.PanBackward();
			}
			else
			{
				cameraController.PanForward();
			}
		}
	}

}
