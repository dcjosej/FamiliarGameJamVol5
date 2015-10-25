using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private static MusicManager _instance;
	public static MusicManager instance;


	public AudioClip[] audioClips;
	public int currentIndexAudioClip { get; set; }

	private AudioSource currentAudioSource;
	private AudioSource previousAudioSource;

	void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		//Invoke("MusicTransition", 5);
		currentIndexAudioClip = 0;

		currentAudioSource = GetComponent<AudioSource>();
		previousAudioSource = GetComponent<AudioSource>();


    }
	
	// Update is called once per frame
	void Update () {
		if(currentAudioSource.isPlaying && GameManager.instance.interacting)
		{
			currentAudioSource.Pause();
		}else if (!GameManager.instance.interacting)
		{
			print("Esto deberia dejar de salir mientras estoy viendo la foto");
			currentAudioSource.UnPause();
		}
	}

	public void CrossFade(AudioClip newTrack, float fadeTime = 0.5f)
	{
		StopAllCoroutines();
		if((GetComponents<AudioSource>()).Length > 1)
		{
			Destroy(GetComponent<AudioSource>());
		}

		AudioSource newAudioSource = instance.gameObject.AddComponent<AudioSource>();
		currentAudioSource = newAudioSource;
		newAudioSource.volume = 0.0f;
		newAudioSource.clip = newTrack;
		newAudioSource.Play();

		StartCoroutine(ActuallyCrossfade(newAudioSource, fadeTime));
	}

	IEnumerator ActuallyCrossfade(AudioSource newSource, float fadeTime)
	{

		float t = 0.0f;

		float initialVolume = GetComponent<AudioSource>().volume;

		while(t < fadeTime)
		{

			GetComponent<AudioSource>().volume = Mathf.Lerp(initialVolume, 0.0f, t / fadeTime);
			newSource.volume = Mathf.Lerp(0.0f, 1.0f, t / fadeTime);
			//GetComponent<AudioSource>().volume = 1.0f - newSource.volume;

			t += Time.deltaTime;
			yield return null;
		}
		Destroy(GetComponent<AudioSource>());
	}

	public void NextMusic()
	{
		currentIndexAudioClip++;
		CrossFade(audioClips[currentIndexAudioClip]);
    }
}
