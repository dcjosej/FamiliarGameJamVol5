using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float rotationVelocity = 1f;
	public float movementVelocity = 4f;

	public IInteractuable interactuableObject { get; set; }

	// Use this for initialization
	void Start ()
	{
		Door [] doors = FindObjectsOfType<Door>();
		foreach(Door door in doors)
		{
			if(door.nextDoor == GameManager.instance.previousRoom)
			{
				transform.position = new Vector3(door.transform.position.x,
					door.transform.position.y,
					door.transform.position.z - 0.8f);
			}
		}
	}

	void Update()
	{
		CheckKeyboard();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!GameManager.instance.interacting)
		{
			float horizontal = Input.GetAxisRaw("Horizontal");
			float vertical = Input.GetAxisRaw("Vertical");

			Rotate(horizontal);
			Move(vertical);
		}
	}

	private void Rotate(float rotation)
	{
		transform.Rotate(0, rotation * rotationVelocity, 0);
	}

	private void Move(float movement)
	{
		Vector3 movementVector = transform.forward * movement * movementVelocity * Time.deltaTime;
		Vector3 newPosition = transform.position + movementVector;
		transform.position = newPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Pill")
		{
			GameManager.instance.DelayDeath();
		}

		if(other.tag == "Interactuable")
		{
			interactuableObject = other.GetComponentInParent<IInteractuable>();
			print("Puedo interactuar con esto!");
		}

		if(other.tag == "CameraTrigger")
		{
			print("Cambiando de camara!");
			Camera cam = other.GetComponentInParent<Camera>();

			cam.enabled = false;
			cam.enabled = true;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Interactuable")
		{
			interactuableObject = null;
			print("Ya no puedo interactuar");
		}
	}

	private void CheckKeyboard()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (interactuableObject != null && !GameManager.instance.interacting)
			{
				interactuableObject.Interact();
			}
			else if(GameManager.instance.zoomedPhoto.IsActive() && GameManager.instance.interacting)
			{
				print("Activando trigger!!: Photo" + GameManager.instance.photoSelected);
				GameManager.instance.zoomedPhoto.gameObject.SetActive(false);
				//GameManager.instance.photoAnimator.SetTrigger("Photo" + GameManager.instance.photoSelected);
				//Invoke("DisablePanelZommedPhoto", 2f);
			}
		}
    }

	private void DisablePanelZommedPhoto()
	{
		GameManager.instance.zoomedPhoto.gameObject.SetActive(false);
	}

}