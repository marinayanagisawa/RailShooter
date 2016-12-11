using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	private float shotSpeed = 3.5f;
	private float lifeTime = 2.0f;
	private GameController gc;
	private int score = 100;
	//private GameObject player;

	void Start(){
		//player = GameObject.Find ("Player");
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		
	}

	void Update () {

		this.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, shotSpeed * Time.deltaTime);
		Destroy (this.gameObject, lifeTime);
	}

	
	void OnTriggerEnter(Collider col){

		Destroy (this.gameObject);

	}
	
}
