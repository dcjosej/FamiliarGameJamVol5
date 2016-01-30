using UnityEngine;
using System.Collections;
using System;

public class LookableItem : Interactive
{

	//public Transform lookPosition; //Camera positions for look object

	private Transform mainCameraPreviousPosition;
	public Transform camDetailTransform;


	public void Start()
	{

		if (!camDetailTransform)
		{
			Debug.LogError("¡Se ha perdido la referencia al transform con la informacion para la camara de detalle!", this);

		}
	}

	public override void Interact()
	{
		GameManager.instance.EnableDetailCamera(camDetailTransform);
	}
}