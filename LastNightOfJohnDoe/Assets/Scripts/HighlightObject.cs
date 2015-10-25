using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour
{

	public Animator parentAnimator;

	public void Start()
	{}

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			parentAnimator.SetBool("Highlight", true);
        }
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			parentAnimator.SetBool("Highlight", false);
		}
	}
}
