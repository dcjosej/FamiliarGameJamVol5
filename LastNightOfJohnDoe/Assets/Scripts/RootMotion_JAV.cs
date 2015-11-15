using UnityEngine;
using System.Collections;

public class RootMotion : MonoBehaviour {

	public Transform playerHead;

	void OnAnimatorMove()
	{
		Animator animator = GetComponent<Animator>();

		print("Runspeed: " + animator.GetFloat("Runspeed"));

		if (animator)
		{
			Vector3 newPosition = transform.position;
			newPosition.z += animator.GetFloat("Runspeed") * Time.deltaTime;
			transform.position = newPosition;
		}
	}
}
