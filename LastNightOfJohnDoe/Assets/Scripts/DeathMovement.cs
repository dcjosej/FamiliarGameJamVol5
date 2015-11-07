using UnityEngine;
using System.Collections;

public class DeathMovement : MonoBehaviour
{
	public Transform neck;
	public Transform[] spawnPoints;

	private Transform targetPlayer;

	void Start()
	{
		targetPlayer = GameObject.FindGameObjectWithTag("TargetPlayer").transform;
        transform.position = spawnPoints[0].position;
	}

	void Update()
	{

		Rotate();

		if(GameManager.instance.lifeBar.value <= 30)
		{
			transform.position = spawnPoints[1].position;
		}
	}

	private void Rotate()
	{
		
		Vector3 toPlayer = (targetPlayer.position - neck.position).normalized;
		toPlayer.y = 0;

		Vector3 dir = neck.transform.up.normalized;
		dir.y = 0;


		float angle = Vector3.Angle(dir, toPlayer);
		print("Angle: " + angle);

		Vector3 crossProduct = Vector3.Cross(dir, toPlayer);


		float nextXAngle = angle + neck.localEulerAngles.x;

		if (crossProduct.y > 0)
		{
			nextXAngle = neck.localEulerAngles.x - angle;
		}

		Quaternion destQuaternion = Quaternion.Euler(nextXAngle, neck.localEulerAngles.y, neck.localEulerAngles.z);


	


		neck.localRotation = Quaternion.Slerp(neck.localRotation, destQuaternion, 8 * Time.deltaTime);

		

		Debug.DrawRay(neck.position, toPlayer * 10, Color.red);
		Debug.DrawRay(neck.position, dir * 2, Color.blue);
	}
}