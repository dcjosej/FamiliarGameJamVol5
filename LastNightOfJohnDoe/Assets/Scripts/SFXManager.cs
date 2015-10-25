using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	private static SFXManager _instance;
	public static SFXManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SFXManager>();
			}
			return _instance;
		}
	}


	private AudioSource audioSource;
	public AudioClip photoSound;



	public void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayPhotoSound()
	{
		audioSource.PlayOneShot(photoSound);
	}
}
