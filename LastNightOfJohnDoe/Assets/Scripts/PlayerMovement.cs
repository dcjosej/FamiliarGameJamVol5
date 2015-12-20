using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class PlayerMovement : MonoBehaviour {

	public float rotationVelocity = 1f;
	public float movementVelocity = 4f;
	private Animator playerAnimator;

	public IInteractuable interactuableObject { get; set; }
	public LookAtIK lookAtIk;


	public Transform lookPoint;
	public Transform normalLookPoint;
	private Transform destLookPoint;
	private float startTime;
	private float journeyLength;

	void OnLevelWasLoaded ()
	{
		InitPlayer();
	}

	void Start()
	{
		InitPlayer();
		destLookPoint = lookPoint;
	}


	private void InitPlayer()
	{
		playerAnimator = GetComponent<Animator>();

		Door[] doors = FindObjectsOfType<Door>();
		foreach (Door door in doors)
		{
			if (door.nextDoor == GameManager.instance.previousRoom)
			{
				foreach (Transform t in door.transform)
				{
					if (t.tag == "SpawnPlayer")
					{
						print("SPAWN PLAYER ENCONTRADO"); 
						transform.position = t.transform.position;
					}
				}
			}
		}

	}
	void Update()
	{
		CheckKeyboard();

	}

	private IEnumerator MoveLookPosition()
	{
		float fracTime = 0f;
		float timeAcum = 0f;
		while (fracTime <= 1)
		{
			lookPoint.transform.position = Vector3.Lerp(lookPoint.transform.position, destLookPoint.transform.position, fracTime);
			timeAcum += Time.deltaTime;
			fracTime = timeAcum / 6;
			yield return null;
		}
	}	
	// Update is called once per frame
	void FixedUpdate () {
		if (!GameManager.instance.interacting)
		{
			float horizontal = Input.GetAxisRaw("Horizontal");
			float vertical = Input.GetAxisRaw("Vertical");

			Rotate(horizontal);
			Move(-vertical);
			MoveLookPosition();
		}
	}

	private void Rotate(float rotation)
	{
		transform.Rotate(0, rotation * rotationVelocity, 0);
	}

	private void Move(float movement)
	{
		playerAnimator.SetFloat("velocity", Mathf.Abs(movement));

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

			if (other.transform.parent == null || other.transform.parent.tag != "Door")
			{
				destLookPoint = other.transform;
				startTime = Time.time;
				StopCoroutine("MoveLookPosition");
				StartCoroutine("MoveLookPosition");
			}

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
			/*TODO: METER ESTO EN UNA FUNCION*/


			if(other.transform.parent == null || other.transform.parent.tag != "Door")
			{
				destLookPoint = normalLookPoint;
				startTime = Time.time;
				journeyLength = Vector3.Distance(destLookPoint.position, lookPoint.transform.position);
				StopCoroutine("MoveLookPosition");
				StartCoroutine("MoveLookPosition");
			}


			interactuableObject = null;
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
				GameManager.instance.zoomedPhoto.gameObject.SetActive(false);
				//GameManager.instance.photoAnimator.SetTrigger("Photo" + GameManager.instance.photoSelected);
				//Invoke("DisablePanelZommedPhoto", 2f);
				int count = 0;
				for(int i = 0; i < GameManager.instance.imagesHolder.Length; i++){

					if (GameManager.instance.imagesHolder[i].sprite != null)
					{
						count++;
					}
				}
			}
		}
    }

	private void DisablePanelZommedPhoto()
	{
		GameManager.instance.zoomedPhoto.gameObject.SetActive(false);
	}
}