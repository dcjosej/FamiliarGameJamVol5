using UnityEngine;
using System.Collections;

public class KeyboardMenu : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Controles");
		}
	}
}
