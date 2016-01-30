using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{

	private Player player;

	public Transform initTransform;
	public Transform destinationTransform;

	private float transitionTime = 2f;


	void Start()
	{
		player = FindObjectOfType<Player>();

		transform.rotation = initTransform.rotation;
		transform.position = initTransform.position;
	}


	void Update()
	{
		//transform.LookAt(player.panCameraTarget);
	}


	public void PanForward()
	{
		//transform.rotation = destinationTransform.rotation;
		StartCoroutine(PanningCoroutine(false));
	}

	public void PanBackward()
	{
		StartCoroutine(PanningCoroutine(true));
	}


	private IEnumerator PanningCoroutine(bool panningBack)
	{

		float time = 0f;
		float step = 0f;
		

		Transform ini = initTransform;
		Transform dest = destinationTransform;



		if (panningBack)
		{
			ini = destinationTransform;
			dest = initTransform;
		}

		


		while (step < 1.0f)
		{

			transform.rotation = Quaternion.Lerp(ini.rotation, dest.rotation, Mathf.SmoothStep(0.0f, 1.0f, step));
			transform.position = Vector3.Lerp(ini.position, dest.position, Mathf.SmoothStep(0.0f, 1.0f, step));

			time += Time.deltaTime;
			step = time / transitionTime;

			yield return null;
		}

		Debug.Log("dwad");

	}

}
