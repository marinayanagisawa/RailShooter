using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	private float shotSpeed = 0.1f;
	private float lifeTime = 9.0f;
	//private GameObject player;

	void Start(){
		//player = GameObject.Find ("Player");
		//this.transform.LookAt (player.transform);
		this.transform.LookAt (Camera.main.transform);
	}

	void Update () {

		this.transform.Translate (0, 0, shotSpeed);
		Destroy (this.gameObject, lifeTime);
	}

	void OnTriggerEnter(Collider col){

		Destroy (this.gameObject);

	}
}
