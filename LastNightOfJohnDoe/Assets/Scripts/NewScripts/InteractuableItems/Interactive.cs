using UnityEngine;

public abstract class Interactive : MonoBehaviour {

	public KeyCode interactKey = KeyCode.K;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.instance.player.AddInteractiveObject(this);
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.instance.player.RemoveInteractiveObject(this);
		}
	}

	public abstract void Interact();
}
