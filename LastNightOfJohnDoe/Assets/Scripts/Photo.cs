using UnityEngine;
using System.Collections;
using System;

public class Photo : MonoBehaviour, IInteractuable {

	public int photoIndex;

	public void Interact()
	{
		GameManager.instance.PutImage(photoIndex);
		print("Interactuando con una foto");
	}
}
