using UnityEngine;
using System.Collections;

public class PanelEvent : MonoBehaviour {
	
	public AudioSource shortSound;
	public AudioSource resultSound;


	void Start(){
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		shortSound = audioSources [0];
		resultSound = audioSources [1];

	}

	public void ShortSound(){
		shortSound.PlayOneShot (shortSound.clip);
	}

	public void ResultSound(){
		resultSound.PlayOneShot (resultSound.clip);
	}
}
