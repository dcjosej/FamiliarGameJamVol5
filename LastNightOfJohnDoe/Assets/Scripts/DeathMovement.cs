using UnityEngine;
using System.Collections;

public class DeathMovement : MonoBehaviour {

	private float velocity;
	private PlayerMovement player;

	void Start ()
	{
		player = FindObjectOfType<PlayerMovement>();
	}
	
	void Update ()
	{
		if (!GameManager.instance.delayingDeath)
		{
			velocity = Vector3.Distance(transform.position, player.transform.position) / GameManager.instance.lifeBar.value;
			Move();
		}
	}

	private void Move()
	{
		Vector3 dir = (player.transform.position - transform.position).normalized;
		transform.position = dir * velocity * Time.deltaTime + transform.position;
	}
}