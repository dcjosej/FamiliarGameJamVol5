using UnityEngine;
using System.Collections;

public class DetailCameraController : MonoBehaviour {

	private Camera camera;
	private float transitionTime = 0.3f;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	public void Transition(CameraData cameraDataDestination)
	{
		StartCoroutine(TransitionCoroutine(cameraDataDestination));
	}

	private IEnumerator TransitionCoroutine(CameraData cameraDataDestination)
	{
		float step = 0f;
		float time = 0f;

		Vector3 initialPosition = transform.position;
		Quaternion initialRotation = transform.rotation;
		float initialFOV = camera.fieldOfView;

		while(step <= 1)
		{
			transform.position = Vector3.Lerp(initialPosition, cameraDataDestination.positionCamera, step);
			transform.rotation = Quaternion.Lerp(initialRotation, cameraDataDestination.rotationCamera, step);
			camera.fieldOfView = Mathf.Lerp(initialFOV, cameraDataDestination.fov, step);

			time += Time.deltaTime;
			step = time / transitionTime;


			yield return null;
		}
	}
}
