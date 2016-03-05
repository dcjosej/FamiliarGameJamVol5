using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

	//public List<Interactive> currentInteractiveObjects { get; set; }
	private int selectedInteractiveIndex = -1;

	[Header("Player Movement")]
	[SerializeField]
	private float rotationVelocity = 1f;
	[SerializeField]
	private float forwardVelocity = 4f;

	[Header("Look Fields")]
	public Transform lookPoint;
	public Transform originLookPoint;
	public Transform focusPoint;
	private float timeLookingTransition = 0.8f;
	private bool isTransitionLookingActive = false;
	private bool lookingVisibleObject = false;

	[Header("Cameras Stuff")]
	public Transform panCameraTarget;

	private int numPills;
	private int numAmmo;


	/// <summary>
	/// Array with interactive objects in this CAMERA. This will use for to determinate what object camera will focus on.
	/// </summary>
	private Interactive[] currentInteractiveObjetcs;
	/// <summary>
	/// Array with interactive objects in this scene. This will use for to determinate what object John Doe will look at.
	/// </summary>
	private Interactive[] interactiveObjectsInScene;
	/// <summary>
	/// Object selected that John Doe is looking at.
	/// </summary>
	private Interactive selectedInteractiveObject;
	/// <summary>
	/// Flag that indicate if John Doe is able to interact with the object that is looking at.
	/// </summary>
	private bool canInteractWithSelectedObject = false;
	/// <summary>
	/// Interactive object that John Doe is colliding. This object might be the same object that he's looking or not.
	/// </summary>
	private Interactive collidingInteractiveObject = null;

	void Start()
	{
		selectedInteractiveIndex = -1;
		
		interactiveObjectsInScene = FindObjectsOfType<Interactive>();
		Array.Sort(interactiveObjectsInScene);

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
		if (selectedInteractiveObject != null && canInteractWithSelectedObject)
		{
			if (Input.GetKeyDown(selectedInteractiveObject.interactKey))
			{
				selectedInteractiveObject.Interact();
			}
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			if (!isTransitionLookingActive)
			{
				LookNextObject();
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isTransitionLookingActive)
			{
				StartCoroutine(LookTransition(lookPoint, originLookPoint, true));
				if (lookingVisibleObject)
				{
					StartCoroutine(LookTransition(focusPoint, originLookPoint, true));
				}
				selectedInteractiveObject = null;
				CheckCanInteractWithSelectedObject();
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


	private void LookNextObject()
	{

		selectedInteractiveIndex++;
		if(selectedInteractiveIndex >= interactiveObjectsInScene.Length)
		{
			selectedInteractiveIndex = 0;
		}

		selectedInteractiveObject = interactiveObjectsInScene[selectedInteractiveIndex];

		Debug.Log("INDICE ENCONTRADO: " + Array.IndexOf(currentInteractiveObjetcs, selectedInteractiveObject));
		
		if (Array.IndexOf(currentInteractiveObjetcs, selectedInteractiveObject) >= 0)
		{
			lookingVisibleObject = true;
			StartCoroutine(LookTransition(focusPoint, selectedInteractiveObject.transform));
		}
		else
		{
			if (lookingVisibleObject)
			{
				StartCoroutine(LookTransition(focusPoint, originLookPoint));
			}
			lookingVisibleObject = false;
		}

		StartCoroutine(LookTransition(lookPoint, selectedInteractiveObject.transform));

		CheckCanInteractWithSelectedObject();
		
	}

	private void CheckCanInteractWithSelectedObject()
	{
		canInteractWithSelectedObject = (selectedInteractiveObject == null || collidingInteractiveObject == null) ? false : selectedInteractiveObject.Equals(collidingInteractiveObject);	
		EnableCorrectActionPanel(selectedInteractiveObject);
	}

	private IEnumerator LookTransition(Transform transformToMove, Transform target, bool playerParent = false)
	{
		float step = 0f;
		float time = 0f;
		Vector3 origin = transformToMove.position;
		Vector3 destination = target.position;
		isTransitionLookingActive = true;

		transformToMove.parent = playerParent ? this.transform : null;

		while(step <= 1.0f)
		{
			transformToMove.position = Vector3.Lerp(origin, destination, step);

			time += Time.deltaTime;
			step = time / timeLookingTransition;
			yield return null;
		}
		isTransitionLookingActive = false;
	}

	/*
	private IEnumerator FocusTransition(Transform target)
	{
		float step = 0f;
		float time = 0f;
		Vector3 origin = lookPoint.position;
		Vector3 destination = target.position;
		isTransitionLookingActive = true;

		while (step <= 1.0f)
		{
			focusPoint.position = Vector3.Lerp(origin, destination, step);
			
			time += Time.deltaTime;
			step = time / timeLookingTransition;

			yield return null;
		}
		isTransitionLookingActive = false;
	}
	*/





	/// <summary>
	/// Metodo para activar el panel correcto de la accion disponible en el HUD.
	/// </summary>
	/// <param name="interactiveObjectType">Objeto activo en este momento. Mostraremos el panel correspondiente en el HUD en funcion de su tipo</param>
	private void EnableCorrectActionPanel(Interactive currentInteractiveObject)
	{

		HUDController.instance.DisablePnLook();
		HUDController.instance.DisablePnOpen();

		/*
		if(currentInteractiveObject is LookableItem)
		{
			HUDController.instance.EnablePnLook(currentInteractiveObject.interactKey);
		}else if(currentInteractiveObject is UsableItem)

		{
			HUDController.instance.EnablePnOpen(currentInteractiveObject.interactKey);
		}
		*/
		if(currentInteractiveObject != null && canInteractWithSelectedObject)
		{
			HUDController.instance.EnablePnOpen(currentInteractiveObject.interactKey);
		}
	}

	/// <summary>
	/// POSIBLE BORRAR
	/// </summary>
	/// <param name="interactiveItem"></param>

	/*
	private void RemoveInteractiveObject(Interactive interactiveItem)
	{
		interactiveObjectsInContact.Remove(interactiveItem);

		if (interactiveObjectsInContact.Count == 0)
		{
			currentIndexInteractiveObjectInContact = -1;
			 = null;

			HUDController.instance.DisablePnLook();
			HUDController.instance.DisablePnOpen();
		}
	}
	*/

	/// <summary>
	/// POSIBLE BORRAR
	/// </summary>
	/// <param name="interactiveObject"></param>
	/// 
	/*
	private void AddInteractiveObject(Interactive interactiveObject)
	{
		interactiveObjectsInContact.Add(interactiveObject);
		currentIndexInteractiveObjectInContact = interactiveObjectsInContact.Count - 1;
		 = interactiveObject;

		EnableCorrectActionPanel(interactiveObject);
	}
	*/

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "CameraTrigger")
		{
			Camera cam = other.GetComponentInParent<Camera>();

			currentInteractiveObjetcs = cam.GetComponent<SceneCamera>().interactiveObjectsInThisCamera;

			cam.enabled = false;
			cam.enabled = true;

			//Reorder by distance the room's interactive elements
			Array.Sort(interactiveObjectsInScene);
		}

		if(other.tag == "Interactuable")
		{
			Interactive interactive = other.GetComponent<Interactive>();
			collidingInteractiveObject = interactive;

			CheckCanInteractWithSelectedObject();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Interactuable")
		{
			//Debug.Log("El objecto " + other.name + " ya no está disponible");
			//RemoveInteractiveObject(other.GetComponent<Interactive>());
			collidingInteractiveObject = null;
			CheckCanInteractWithSelectedObject();
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