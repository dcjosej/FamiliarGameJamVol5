using UnityEngine;
using System.Collections;

public class TransitionByDistance : MonoBehaviour {

	private Player player;

	public Transform initTransform;
	public Transform destinationTransform;
	public Transform destinationTransformForDistance;



	private bool transitioning = false;
	private float distance = 0f;

	void Start()
	{
		player = FindObjectOfType<Player>();

		transform.rotation = initTransform.rotation;
		transform.position = initTransform.position;
	}

	


	public void InitTransition()
	{
		transitioning = true;
		distance = player.transform.position.z - destinationTransformForDistance.position.z;
		StartCoroutine(TransitionCoroutine());
	}

	private IEnumerator TransitionCoroutine()
	{
		float step = 0;
		while(step <= 1)
		{

			transform.position = Vector3.Lerp(initTransform.position, destinationTransform.position, step);
			transform.rotation = Quaternion.Lerp(initTransform.rotation, destinationTransform.rotation, step);

			//float currentDistance = Vector3.Distance(player.transform.position, destinationTransform.position);

			float currentDistance = Mathf.Abs(player.transform.position.z - destinationTransformForDistance.position.z);

			if (currentDistance <= 0.01f)
			{
				currentDistance = 0f;
			}
            step = 1 - currentDistance / distance;

			yield return null;
		}
	}

}
