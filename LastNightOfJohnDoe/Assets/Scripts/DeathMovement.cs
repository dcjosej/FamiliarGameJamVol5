using UnityEngine;
using System.Collections;

public class DeathMovement : MonoBehaviour {

	private float velocity;
	private PlayerMovement player;
	private Animator deathAnimator;


	public Transform[] spawnPoints;

	public int state = 0;
	public int selectedIndex = 0;
	public bool attacking = false;

	void Start ()
	{
		player = FindObjectOfType<PlayerMovement>();

		selectedIndex = Mathf.Clamp(state, 0, spawnPoints.Length - 1);
		transform.position = spawnPoints[selectedIndex].position;
	}
	
	void Update ()
	{
		if (!GameManager.instance.delayingDeath)
		{
			velocity = Vector3.Distance(transform.position, player.transform.position) / GameManager.instance.lifeBar.value;
			Move();
		}

		/*CAMBIAR AQUI*/
		if(GameManager.instance.lifeBar.value < 30 && GameManager.instance.lifeBar.value >= 15 && state == 0)
		{
			state++;
			selectedIndex = Mathf.Clamp(state, 0, spawnPoints.Length - 1);
			transform.position = spawnPoints[selectedIndex].position;
		}else if (GameManager.instance.lifeBar.value < 15 && state == 1)
		{
			state++;
        }

	}

	private void Move()
	{
		if (state == 2)
		{
			print("MOVIENDO LA MUERTE!");
			Vector3 dir = (player.transform.position - transform.position).normalized;
			transform.position = dir * velocity * Time.deltaTime + transform.position;

			print("Transform position muerte: " + transform.position);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		print("On trigger enter!: " + other.tag);

		if(other.tag == "DeathAttack")
		{
			deathAnimator.SetTrigger("Attack");
			velocity = 0f;
		}
	}
}