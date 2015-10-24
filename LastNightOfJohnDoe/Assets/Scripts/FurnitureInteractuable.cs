using UnityEngine;
using System.Collections;
using System;

public class FurnitureInteractuable : MonoBehaviour, IInteractuable {

	public GameObject objectInside;

	public void Interact()
	{
		//GameObject go = Instantiate(objectInside);
		GameManager.instance.ShowGUIPill();
		print("Rebuscando en el mueble!");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
