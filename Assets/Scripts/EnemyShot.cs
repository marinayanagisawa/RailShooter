using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	private float shotSpeed = 1.5f;
	private float lifeTime = 8.0f;
	private GameController gc;
	private int score = 1000;
	//private GameObject player;
	public GameObject par;

	void Start(){
		//player = GameObject.Find ("Player");
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		Destroy(this.gameObject, lifeTime);

	}

	void Update () {

		this.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, shotSpeed * Time.deltaTime);
		
	}

	
	void OnTriggerEnter(Collider col){
		string layerName = LayerMask.LayerToName(col.gameObject.layer);

		if (layerName == "PlayerShot") {
			PlayParticle (col.gameObject);
			Destroy (this.gameObject);
			gc.addScore (score);
		}
	}


	void PlayParticle(GameObject shot){
		Object parObj = Instantiate (par, shot.transform.position, shot.transform.rotation);
		Destroy (parObj, 1.5f);
	}

}
