using UnityEngine;
using System.Collections;

public class PillLogic : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
