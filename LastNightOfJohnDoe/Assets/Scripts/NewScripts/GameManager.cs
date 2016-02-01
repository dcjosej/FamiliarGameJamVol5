using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; set; }


	public enum GameState { Normal, Detail}
	public GameState gameState = GameState.Normal;


	public Player player { get; set; }
	public Camera detailCamera { get; set; }


	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		player = FindObjectOfType<Player>();
		//detailCamera = GameObject.FindGameObjectWithTag("DetailCam").GetComponent<Camera>();
	}
	

	public void EnableDetailCamera(Transform camDetailTransform)
	{ 
		detailCamera.transform.position = camDetailTransform.position;
		detailCamera.transform.rotation = camDetailTransform.rotation;
		detailCamera.enabled = true;

		ChangeState(GameState.Detail);
	}

	public void DisableDetailCamera()
	{
		detailCamera.enabled = false;

		ChangeState(GameState.Normal);
	}

	private void ChangeState(GameState gameState)
	{

		this.gameState = gameState;

		switch (gameState)
		{
			case GameState.Detail:
				HUDController.instance.DisablePnActions();
				break;
			case GameState.Normal:
				HUDController.instance.EnablePnActions();
				break;
		}
	}
}