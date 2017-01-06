using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Animator anim;
	public AudioSource sound;

	void OnTriggerEnter(Collider col){
		string layerMask = LayerMask.LayerToName (col.gameObject.layer);

		if (layerMask == "Player") {
			Debug.Log ("DoorOpen!");
			anim.SetTrigger ("DoorOpen");

			Invoke("OpenSound", 0.1f);
		}
	}


	public void OpenSound(){
		sound.PlayOneShot (sound.clip);
	}

}
