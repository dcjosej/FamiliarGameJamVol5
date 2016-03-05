using System;
using UnityEngine;

public abstract class Interactive : MonoBehaviour, IComparable<Interactive>, IEquatable<Interactive> {

	public KeyCode interactKey = KeyCode.K;

	private static Player player;

	void Start()
	{
		player = FindObjectOfType<Player>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			//GameManager.instance.player.AddInteractiveObject(this);
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			//GameManager.instance.player.RemoveInteractiveObject(this);
		}
	}

	public abstract void Interact();



	public int CompareTo(Interactive other)
	{

		int res = 0;

		if(Vector3.Distance(other.transform.position, player.originLookPoint.position) < Vector3.Distance(transform.position, player.originLookPoint.position))
		{
			res = 1;
		}
		else
		{
			res = -1;
		}

		return res;
		 
	}

	//TODO: Revisar esta igualdad
	public bool Equals(Interactive other)
	{

		bool res = false;

		if(other != null && other.gameObject.name == gameObject.name)
		{
			res = true;
		}
		return res;
	}
}
