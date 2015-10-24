using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private static MusicManager _instance;
	public static MusicManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<MusicManager>();
			}
			return _instance;
		}
	}

	public AudioClip audio1;
	public AudioClip audio2;
	public AudioClip audio3;

	// Use this for initialization
	void Start () {
		Invoke("MusicTransition", 5);
	}

	private void MusicTransition()
	{
		CrossFade(audio2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CrossFade(AudioClip newTrack, float fadeTime = 1.0f)
	{
		AudioSource newAudioSource = instance.gameObject.AddComponent<AudioSource>();

		

		newAudioSource.volume = 0.0f;
		newAudioSource.clip = newTrack;
		newAudioSource.Play();

		StartCoroutine(ActuallyCrossfade(newAudioSource, fadeTime));
	}

	IEnumerator ActuallyCrossfade(AudioSource newSource, float fadeTime)
	{
		float t = 0.0f;

		while(t < fadeTime)
		{
			newSource.volume = Mathf.Lerp(0.0f, 1.0f, t / fadeTime);
			GetComponent<AudioSource>().volume = 1.0f - newSource.volume;

			t += Time.deltaTime;
			yield return null;
		}
	}
	
}
