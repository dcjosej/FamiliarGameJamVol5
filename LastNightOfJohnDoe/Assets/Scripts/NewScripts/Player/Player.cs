using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

	public List<Interactive> currentInteractiveObjects { get; set; }
	private int selectedInteractiveIndex = -1;

	[Header("Player Movement")]
	[SerializeField]
	private float rotationVelocity = 1f;
	[SerializeField]
	private float forwardVelocity = 4f;

	[Header("Cameras Stuff")]
	public Transform panCameraTarget;

	private int numPills;
	private int numAmmo;

	/// <summary>
	/// Array with interactive objects in this scene. This will use for to determinate what object John Doe will look at.
	/// </summary>
	private Interactive[] interactiveObjectsInScene;
	/// <summary>
	/// Object selected that John Doe is looking at.
	/// </summary>
	private Interactive selectedInteractiveObject;

	void OnLevelWasLoaded()
	{
		interactiveObjectsInScene = FindObjectsOfType<Interactive>();
	}
	
	void Start()
	{
		currentInteractiveObjects = new List<Interactive>();
		selectedInteractiveIndex = -1;

		numPills = 0;
		numAmmo = 0;
	}


	void FixedUpdate()
	{


		/* TODO: State pattern?? */
		switch (GameManager.instance.gameState)
		{
			case GameManager.GameState.Normal:
				ProcessInputForNormalMode();
				break;
			case GameManager.GameState.Detail:
				ProcessInputForDetailMode();
				break;
		}
	}


	#region PROCESS INPUT

	private void ProcessInputForNormalMode()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Rotate(horizontal);
		MoveForward(-vertical);


		/* Comprobamos si se ha pulsado algunas de las teclas de los objetos disponibles para interactuar */
		if(selectedInteractiveObject != null)
		{
			if (Input.GetKeyDown(selectedInteractiveObject.interactKey))
			{
				selectedInteractiveObject.Interact();
			}
		}
	}

	private void ProcessInputForDetailMode()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameManager.instance.DisableDetailCamera();
		}
	}


	#endregion


	#region INTERACTION WITH ENVIRONMENT
	public void AddInteractiveObject(Interactive interactiveObject)
	{
		currentInteractiveObjects.Add(interactiveObject);
		selectedInteractiveIndex = currentInteractiveObjects.Count - 1;
		selectedInteractiveObject = interactiveObject;

		EnableCorrectActionPanel(interactiveObject);
	}

	/// <summary>
	/// Metodo para activar el panel correcto de la accion disponible en el HUD.
	/// </summary>
	/// <param name="interactiveObjectType">Objeto activo en este momento. Mostraremos el panel correspondiente en el HUD en funcion de su tipo</param>
	private void EnableCorrectActionPanel(Interactive currentInteractiveObject)
	{

		HUDController.instance.DisablePnLook();
		HUDController.instance.DisablePnOpen();

		if(currentInteractiveObject is LookableItem)
		{
			HUDController.instance.EnablePnLook(currentInteractiveObject.interactKey);
		}else if(currentInteractiveObject is UsableItem)
		{
			HUDController.instance.EnablePnOpen(currentInteractiveObject.interactKey);
		}
	}

	public void RemoveInteractiveObject(Interactive interactiveItem)
	{
		currentInteractiveObjects.Remove(interactiveItem);

		if(currentInteractiveObjects.Count == 0)
		{
			selectedInteractiveIndex = -1;
			selectedInteractiveObject = null;

			HUDController.instance.DisablePnLook();
			HUDController.instance.DisablePnOpen();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "CameraTrigger")
		{
			Camera cam = other.GetComponentInChildren<Camera>();

			cam.enabled = false;
			cam.enabled = true;
		}
	}
		

#endregion


#region PLAYER MOVEMENT
private void Rotate(float rotation)
	{
		transform.Rotate(0, rotation * rotationVelocity, 0);
	}

	private void MoveForward(float movement)
	{
		Vector3 movementVector = transform.forward * movement * forwardVelocity * Time.deltaTime;
		Vector3 newPosition = transform.position + movementVector;

		transform.position = newPosition;
	}
	#endregion
}
