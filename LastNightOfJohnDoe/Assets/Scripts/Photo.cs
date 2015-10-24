using UnityEngine;
using System.Collections;
using System;

public class Photo : MonoBehaviour, IInteractuable {

	public int photoIndex;

	public void Interact()
	{
		SFXManager.instance.PlayPhotoSound();
		GameManager.instance.interacting = true;
		GameManager.instance.PutImage(photoIndex);
		print("Interactuando con una foto");
	}
}
