using UnityEngine;
using System.Collections;
using System;

public class FurnitureInteractuable : Interactive
{
	
	public GameObject objectInside;
	public bool collected;

	public override void Interact()
	{
		//if (!collected)
		//{
		//	//GameObject go = Instantiate(objectInside);
		//	GameManager.instance.ShowGUIPill();
		//}
		//else
		//{
		//	GameManager.instance.ShowNothing();
		//}

		//GameManager.instance.currentObject = gameObject;
		//GameManager.instance.interacting = true;
	}
	
	public void Start()
	{}
}
