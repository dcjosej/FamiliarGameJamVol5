using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float rotationVelocity = 1f;
	public float movementVelocity = 4f;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Rotate(horizontal);
		Move(vertical);
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
	}
}