using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

	private Quaternion rotation;
	
	void Awake () {
		rotation = transform.localRotation;
	}
	
	void LateUpdate () {
		transform.localRotation = rotation;
	}
}
