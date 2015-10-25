using UnityEngine;
using System.Collections;

public class KeyboardFinalmalo : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Hall");
		}
	}
}
