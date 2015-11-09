using UnityEngine;
using System.Collections;

public class RootMotion : MonoBehaviour {


	void OnAnimatorMove()
	{
		Animator animator = GetComponent<Animator>();

		print("Runspeed: " + animator.GetFloat("Runspeed"));

		if (animator)
		{
			Vector3 newPosition = transform.position;
			newPosition.z += animator.GetFloat("Runspeed") * Time.deltaTime * 10;
			transform.position = newPosition;
		}
	}
}
