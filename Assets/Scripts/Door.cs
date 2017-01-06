using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Animator anim;

	void OnTriggerEnter(Collider col){
		Debug.Log ("DoorOpen!");
		anim.SetTrigger ("DoorOpen");
	}
}
