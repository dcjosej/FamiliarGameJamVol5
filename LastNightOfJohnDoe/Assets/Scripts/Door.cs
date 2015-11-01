using UnityEngine;
using System.Collections;
using System;


public enum Room { SALON, ASEO, COCINA, DORMITORIO, HALL, PASILLO }

public class Door : MonoBehaviour, IInteractuable{

	public bool closed;
	public Room nextDoor;

	public void Interact()
	{

		GameManager.instance.nextRoom = nextDoor;
		ChangePreviousRoom();

		if (!closed)
		{
			switch (nextDoor)
			{
				case Room.ASEO:
					print("Entrando en el aso");
					Application.LoadLevel("Aseo");
					break;
				case Room.COCINA:
					print("Entrando en el aso");
					Application.LoadLevel("Cocina");
					break;
				case Room.DORMITORIO:
					Application.LoadLevel("Dormitorio");
					break;
				case Room.HALL:
					Application.LoadLevel("Hall");
					break;
				case Room.SALON:
					Application.LoadLevel("Salon");
					break;
				case Room.PASILLO:
					Application.LoadLevel("Pasillo");
					break;
			}
		}
	}

	private void ChangePreviousRoom()
	{
		switch(Application.loadedLevelName){
			case "Pasillo":
				GameManager.instance.previousRoom = Room.PASILLO;
				break;
			case "Salon":
				GameManager.instance.previousRoom = Room.SALON;
				break;
			case "Aseo":
				GameManager.instance.previousRoom = Room.ASEO;
				break;
			case "Hall":
				GameManager.instance.previousRoom = Room.HALL;
				break;
			case "Cocina":
				GameManager.instance.previousRoom = Room.COCINA;
				break;
			case "Dormitorio":
				GameManager.instance.previousRoom = Room.DORMITORIO;
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
