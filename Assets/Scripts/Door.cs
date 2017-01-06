using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Animator anim;

	void OnTriggerEnter(Collider col){
		string layerMask = LayerMask.LayerToName (col.gameObject.layer);

		if (layerMask == "Player") {
			Debug.Log ("DoorOpen!");
			anim.SetTrigger ("DoorOpen");
		}
	}
}
