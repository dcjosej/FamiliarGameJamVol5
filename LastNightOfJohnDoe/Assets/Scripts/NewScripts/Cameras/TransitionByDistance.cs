using UnityEngine;
using System.Collections;

public class TransitionByDistance : MonoBehaviour {

	private Player player;

	public Transform initTransform;
	public Transform destinationTransform;
	public Transform destinationTransformForDistance;

	public Transform[] points;


	private bool transitioning = false;
	//private float distance = 0f;
	private float transitionTime = 5f;

	void Start()
	{
		player = FindObjectOfType<Player>();

		transform.rotation = points[0].rotation;
		transform.position = points[0].position;
	}

	public void InitTransition()
	{
		transitioning = true;
		//distance = player.transform.position.z - destinationTransformForDistance.position.z;
		StartCoroutine(TransitionCoroutine());
	}

	private IEnumerator TransitionCoroutine()
	{

		
		for(int i = 0; i < points.Length - 1; i++)
		{

			initTransform = points[i];
			destinationTransform = points[i + 1];

			float step = 0;
			float time = 0;

			while (step <= 1)
			{

				transform.position = Vector3.Lerp(initTransform.position, destinationTransform.position, step);
				transform.rotation = Quaternion.Lerp(initTransform.rotation, destinationTransform.rotation, step);

				//float currentDistance = Vector3.Distance(player.transform.position, destinationTransform.position);

				float currentDistance = Mathf.Abs(player.transform.position.z - destinationTransformForDistance.position.z);

				if (currentDistance <= 0.01f)
				{
					currentDistance = 0f;
				}

				time += Time.deltaTime;
				step = time / transitionTime;

				yield return null;
			}
		}
	}

}
