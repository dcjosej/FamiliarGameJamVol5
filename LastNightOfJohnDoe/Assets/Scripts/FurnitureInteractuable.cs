using UnityEngine;
using System.Collections;
using System;

public class FurnitureInteractuable : MonoBehaviour, IInteractuable
{
	
	public GameObject objectInside;
	public bool collected;

	public void Interact()
	{
		if (!collected)
		{
			//GameObject go = Instantiate(objectInside);
			GameManager.instance.ShowGUIPill();
		}
		else
		{
			GameManager.instance.ShowNothing();
		}

		GameManager.instance.currentObject = gameObject;
		GameManager.instance.interacting = true;
	}
	
	public void Start()
	{}
}
