using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour
{

	public Animator parentAnimator;

	public void Start()
	{}

	public void OnTriggerEnter(Collider other)
	{
		print("El tag del objeto con el que colisiono: " + other.tag);

		if(other.tag == "Player")
		{
			print("RESALTANDO FOTO!!");
			parentAnimator.enabled = true;
		}
	}
}
