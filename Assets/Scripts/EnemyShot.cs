using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	private float shotSpeed = 1.5f;
	private float lifeTime = 8.0f;
	//private GameController gc;
	private int score = 100;
	//private GameObject player;

	void Start(){
		//player = GameObject.Find ("Player");
		//gc = GameObject.Find("GameController").GetComponent<GameController>();
		Destroy(this.gameObject, lifeTime);

	}

	void Update () {

		this.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, shotSpeed * Time.deltaTime);
		
	}

	
	void OnTriggerEnter(Collider col){

		Destroy (this.gameObject);

	}
	
}
