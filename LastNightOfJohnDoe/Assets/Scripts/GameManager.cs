using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

	public float LifeTime = 10f;
	public Slider lifeBar;
	public float RESTART_DEATH_TIME = 4f;
	private float acumTime = 0;
	private bool delayingDeath = false;

	void Start ()
	{
		lifeBar.maxValue = LifeTime;
	}


	void Update ()
	{
		if (!delayingDeath)
		{
			UpdateLifeTime();
			acumTime += Time.deltaTime;
		}
	}

	public void DelayDeath()
	{
		delayingDeath = true;
		Invoke("RestartDeath", RESTART_DEATH_TIME);
	}

	private void RestartDeath()
	{
		print("Restaurando la muerte!");
		delayingDeath = false;
	}

	private void UpdateLifeTime()
	{
		lifeBar.value = LifeTime - acumTime;
	}
}