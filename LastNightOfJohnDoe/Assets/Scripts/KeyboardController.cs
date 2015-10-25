using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour
{
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Hall");
		}
	}
}
