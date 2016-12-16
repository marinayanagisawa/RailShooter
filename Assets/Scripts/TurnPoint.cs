using UnityEngine;
using System.Collections;

public class TurnPoint : MonoBehaviour {

	private PlayerController pc;

	//回転したい角度をインスペクターから入力
	public int euler;

	void Start () {
		pc = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}
	
	void OnTriggerEnter(Collider col){
		string layerMask = LayerMask.LayerToName (col.gameObject.layer);

		if (layerMask == "Player") {
			pc.CameraTurn (euler);
		}
	}
}
