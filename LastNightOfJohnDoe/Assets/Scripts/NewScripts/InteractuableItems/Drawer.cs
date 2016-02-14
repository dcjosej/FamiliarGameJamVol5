using UnityEngine;
using System.Collections;
using System;

public class Drawer : MonoBehaviour {


	//TODO: Utilizar find child
	public Transform destinationTransform;
	public bool isTransitioning = false;
	public Transform drawerRenderer;
	public Transform drawerContent;

	[SerializeField]
	private int maximumCapacity = 4; //MAXIMUM number of objects than can be placed into the drawer
	public int currentNumberOfObject { get; set; } //Number of objects that are currently placed into the drawer

	private CameraData cameraDataDestination;
	private DetailCameraController detailCameraController;

	//TODO: ¿Quizas mejor dos transform y posicion Z y X random?
	public Transform[] spawnPointsForContentObjects;


	//Transition fields
	private Vector3 initPosition;
	private Quaternion initRotation;
	private float openingTime = 0.2f;
	private float tiltingTime = 0.2f;

	void Start () {
		initPosition = drawerRenderer.position;
		initRotation = drawerRenderer.rotation;
		detailCameraController = FindObjectOfType<DetailCameraController>();
		cameraDataDestination = GetComponentInChildren<CameraData>();
		currentNumberOfObject = 0;
		if (drawerContent == null)
		{
			Debug.LogError("Reference missing of Drawer Content!");
		}
	}

	public void OpenDrawer()
	{
		StartCoroutine(Sequence(OpencCloseDrawer(true), TiltDrawer(true)));
		detailCameraController.Transition(cameraDataDestination);
	}

	public void PlaceRandomObject(GameObject objectToInstantiate)
	{
		/* TODO: Instantiate object in random position */
		currentNumberOfObject++;
		throw new NotImplementedException();
	}

	public void CloseDrawer()
	{
		StartCoroutine(Sequence(TiltDrawer(false), OpencCloseDrawer(false)));
	}

	//TODO: TRASLADAR ESTE METODO A UNA CLASE DE UTILIDADES
	private IEnumerator Sequence(params IEnumerator[] sequence)
	{
		for(int i = 0; i < sequence.Length; i++)
		{
			while (sequence[i].MoveNext())
			{
				yield return sequence[i].Current;
			}
		}
	}


	#region ANIMATIONS COROUTINES
	private IEnumerator OpencCloseDrawer(bool open)
	{
		float time = 0f;
		float step = 0f;
		while(step <= 1f)
		{
			drawerRenderer.position = open ? Vector3.Lerp(initPosition, destinationTransform.position, step) : Vector3.Lerp(destinationTransform.position, initPosition, step);

			time += Time.deltaTime;
			step = time / openingTime;
			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator TiltDrawer(bool open)
	{
		float time = 0f;
		float step = 0f;

		
		while (step <= 1f)
		{
			drawerRenderer.rotation = open ? Quaternion.Lerp(initRotation, destinationTransform.rotation, step) : Quaternion.Lerp(destinationTransform.rotation, initRotation, step);

			time += Time.deltaTime;
			step = time / tiltingTime;
			yield return new WaitForFixedUpdate();
		}
	}
	#endregion
}
