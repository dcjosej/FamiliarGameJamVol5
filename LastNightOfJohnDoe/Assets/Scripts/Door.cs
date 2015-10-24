using UnityEngine;
using System.Collections;
using System;


public enum NextDoor { ASEO, COCINA, DORMITORIO, HALL, SALON }

public class Door : MonoBehaviour, IInteractuable{

	public bool closed;
	public NextDoor nextDoor; 

	public void Interact()
	{
		switch (nextDoor)
		{
			case NextDoor.ASEO:
				print("Entrando en el aso");
				Application.LoadLevel("Aseo");
				break;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
