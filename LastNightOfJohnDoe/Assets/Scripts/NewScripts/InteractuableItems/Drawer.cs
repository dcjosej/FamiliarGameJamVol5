using UnityEngine;
using System.Collections;
//using System;

public class Drawer : MonoBehaviour {

	//TODO: Si no tenemos content, lo creamos
	//TODO: Utilizar find child
	//public Transform destinationTransform;
	public bool isTransitioning = false;
	public bool onlyRotate = false;
	private Transform drawerRenderer;

	[SerializeField]
	private Transform drawerContent;

	[SerializeField]
	private int maximumCapacity = 4; //MAXIMUM number of objects than can be placed into the drawer
	public int currentNumberOfObject { get; set; } //Number of objects that are currently placed into the drawer
	[SerializeField]
	private Transform upperLeft;
	[SerializeField]
	private Transform bottomRight;

	private CameraData cameraDataDestination;
	private DetailCameraController detailCameraController;

	//TODO: ¿Quizas mejor dos transform y posicion Z y X random?
	public Transform[] spawnPointsForContentObjects;


	//Transition fields
	private Vector3 initPosition;
	private Quaternion initRotation;
	private float openingTime = 0.3f;
	private float tiltingTime = 0.4f;


	void Start () {


		drawerRenderer = GetComponentInChildren<MeshFilter>().transform;


		initPosition = drawerRenderer.localPosition;
		initRotation = drawerRenderer.localRotation;
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
		if (onlyRotate)
		{
			StartCoroutine(Sequence(TiltDrawer(true)));
		}
		else
		{
			StartCoroutine(Sequence(OpencCloseDrawer(true), TiltDrawer(true)));
		}
		detailCameraController.Transition(cameraDataDestination);
	}

	public void PlaceRandomObject(GameObject objectToInstantiate)
	{


		Vector3 randomPositionIntoDrawer = new Vector3(Random.Range(bottomRight.localPosition.x, upperLeft.localPosition.x), Random.Range(bottomRight.localPosition.y, upperLeft.localPosition.y), bottomRight.localPosition.z);
		GameObject go = (GameObject)Instantiate(objectToInstantiate, Vector3.zero, Quaternion.identity);

		go.transform.parent = drawerContent.parent;
		go.transform.localPosition = randomPositionIntoDrawer;
		go.transform.parent = drawerContent;
		
		currentNumberOfObject++;

	}

	public void CloseDrawer()
	{

		//TODO: Dar una vuelta a esto
		if (onlyRotate)
		{
			StartCoroutine(Sequence(TiltDrawer(false)));
		}
		else
		{
			StartCoroutine(Sequence(TiltDrawer(false), OpencCloseDrawer(false)));
		}
		
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
			drawerRenderer.localPosition = open ? Vector3.Lerp(initPosition, Vector3.zero, step) : Vector3.Lerp(Vector3.zero, initPosition, step);

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
			drawerRenderer.localRotation = open ? Quaternion.Lerp(initRotation, Quaternion.Euler(0, 0, 0), step) : Quaternion.Lerp(Quaternion.Euler(0, 0, 0), initRotation, step);

			time += Time.deltaTime;
			step = time / tiltingTime;
			yield return new WaitForFixedUpdate();
		}
	}
	#endregion
}
