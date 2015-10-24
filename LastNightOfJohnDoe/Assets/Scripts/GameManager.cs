using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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

	public float LifeTime = 60f;
	public Slider lifeBar;
	public float RESTART_DEATH_TIME = 4f;
	private float acumTime = 0;
	public bool delayingDeath { get; set; }

	/* Referencias a elementos de la GUI */
	public GameObject CanvasPillGUI;
	public Canvas grayCanvas;
	public GameObject albumCanvas;
	public Sprite[] photos;
	public Image[] imagesHolder;
	public Image zoomedPhoto;
	public Animator photoAnimator;
	public int photoSelected { get; set; }

	/* Variables para pausar el juego mientras se esta interactuando */
	public bool interacting = false;

	public Room previousRoom { get; set; }
	public Room nextRoom { get; set; }

	/* Animators Controllers  y GUI */
	public Animator pillGUIAnimator;
	
	void Awake()
	{
		DontDestroyOnLoad(this);
		previousRoom = Room.SALON;


	}

	void Start ()
	{
		delayingDeath = false;
		lifeBar.maxValue = LifeTime;
	}


	void Update ()
	{
		if (!interacting)
		{
			if (!delayingDeath)
			{
				UpdateLifeTime();
				acumTime += Time.deltaTime;
			}
		}

		CheckKeyboard();
	}

	private void CheckKeyboard()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			interacting = false;
			albumCanvas.SetActive(false);
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

	/* FUNCIONES PARA BOTONES */
	public void TakePill()
	{
		CanvasPillGUI.gameObject.SetActive(false);
		grayCanvas.gameObject.SetActive(false);
		print("He tomado la pastilla!");
	}

	public void RefusePill()
	{
		grayCanvas.gameObject.SetActive(false);
		CanvasPillGUI.SetActive(false);
		print("No quiero la pastilla...");
	}

	public void ShowGUIPill()
	{
		CanvasPillGUI.SetActive(true);
		grayCanvas.gameObject.SetActive(true);
	}

	public void PutImage(int photoIndex)
	{

		photoSelected = photoIndex;

		imagesHolder[photoIndex].sprite = photos[photoIndex];
		albumCanvas.gameObject.SetActive(true);
		zoomedPhoto.sprite = photos[photoIndex];
		//photoAnimator.SetTrigger("Photo" + photoIndex);
    }

	public void ClickPhoto(int index)
	{
		print("Pinchando en una foto: " + index);
		//imagesHolder[index].transform.localScale
	}
}