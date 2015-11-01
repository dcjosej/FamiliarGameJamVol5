using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager instance;

	public float LifeTime = 60f;
	public Slider lifeBar;
	public float RESTART_DEATH_TIME = 4f;
	private float acumTime = 0;
	public bool delayingDeath { get; set; }

	/* Referencias a elementos de la GUI */
	public GameObject CanvasPillGUI;
	//public Canvas grayCanvas;
	public GameObject albumCanvas;
	public Sprite[] photos;
	public Image[] imagesHolder;
	public Image zoomedPhoto;
	public Animator photoAnimator;
	public Text labelText; //Texto para mostrar cuando no hay nada en los cajones
	public int photoSelected { get; set; }

	public GameObject currentObject;

	/* Variables para pausar el juego mientras se esta interactuando */
	public bool interacting = false;

	public Room previousRoom { get; set; }
	public Room nextRoom { get; set; }

	public int unlockedPhotos = 0;

	/* Animators Controllers  y GUI */
	//public Animator pillGUIAnimator;
	
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
		previousRoom = Room.HALL;
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

		if(lifeBar.value <= 30 && MusicManager.instance.currentIndexAudioClip == 0)
		{
			MusicManager.instance.NextMusic();
		}else if(lifeBar.value <= 15 && MusicManager.instance.currentIndexAudioClip == 1)
		{
			MusicManager.instance.NextMusic();
		}

		CheckKeyboard();

		if(lifeBar.value <= 0)
		{
			Application.LoadLevel("FinalMalo");
		}

	}

	public void ShowNothing()
	{
		labelText.text = "Nothing...";
		labelText.transform.parent.gameObject.SetActive(true);
    }

	private void CheckKeyboard()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			interacting = false;
			albumCanvas.SetActive(false);
			labelText.transform.parent.gameObject.SetActive(false);


			GameManager.instance.zoomedPhoto.gameObject.SetActive(true);
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
		print("Pulsando boton!");
		CanvasPillGUI.gameObject.SetActive(false);
		//grayCanvas.gameObject.SetActive(false);
		interacting = false;
		DelayDeath();

		currentObject.GetComponent<FurnitureInteractuable>().collected = true;
    }

	public void RefusePill()
	{
		//grayCanvas.gameObject.SetActive(false);
		CanvasPillGUI.SetActive(false);
		interacting = false;
		print("No quiero la pastilla...");
	}

	public void ShowGUIPill()
	{
		CanvasPillGUI.SetActive(true);
		//grayCanvas.gameObject.SetActive(true);
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