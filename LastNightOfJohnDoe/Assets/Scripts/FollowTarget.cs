using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {


	public Transform target;
	public float velocity;
	

	void Start () {
		velocity = 4f;
	}

	void Update () {


		Vector3 dir = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(-dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, velocity * Time.deltaTime);

		/*
		Vector3 auxVec = target.position - transform.position;
		float angle = Vector3.Angle(transform.up, auxVec);

		Debug.DrawRay(transform.position, auxVec * 4, Color.magenta);

		Quaternion rot = Quaternion.LookRotation(Vector3.up, -Vector3.right);
		rot.



		if(angle > 0.5f)
		{
			transform.Rotate(new Vector3(1, 0, 0), angle);
		}

		/*
		Vector3 localPos = transform.InverseTransformDirection(target.position - transform.position);
		localPos.y = 0;

		Vector3 lookPos = transform.position + transform.TransformDirection(localPos);



		transform.LookAt(lookPos, Vector3.up);
		*/

		//transform.Rotate(0, 80, 80);


		/*
		print("dadwadw");
		Vector3 relativePos = target.position - transform.position;
		float angle = Vector3.Angle(transform.forward, relativePos);

		transform.Rotate(0, angle, 0);
		*/

		//transform.LookAt(target);
		//print("Rotacion: " + transform.rotation);

		//print("Nombre: " + transform.name);

		//transform.rotation = rotation;
	}
}
