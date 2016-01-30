using UnityEngine;
using System.Collections;

public class Persistente : MonoBehaviour
{

	private static Persistente _instance;
	public static Persistente instance;


	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
