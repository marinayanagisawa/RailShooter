using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	public float shotSpeed = 4.0f;
	private float lifeTime = 8.0f;
	private int score = 1000;
	private GameController gc;
	public GameObject par;
	//private GameObject playerCol;

	void Start(){

		//playerCol = GameObject.Find ("playerCollider");
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		Destroy(this.gameObject, lifeTime);

	}

	void Update () {

		this.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, shotSpeed * Time.deltaTime);
		//this.transform.position = Vector3.MoveTowards(transform.position, playerCol.transform.position, shotSpeed * Time.deltaTime);
	}

	
	void OnTriggerEnter(Collider col){
		string layerName = LayerMask.LayerToName(col.gameObject.layer);

		if (layerName == "PlayerShot") {
			PlayParticle (col.gameObject);
			Destroy (this.gameObject);
			gc.addScore (score);
		} else {
			Destroy (this.gameObject);
		}
	}

	//弾同士がヒットした場所にパーティクルのプレファブを呼ぶ
	void PlayParticle(GameObject shot){
		Object parObj = Instantiate (par, shot.transform.position, shot.transform.rotation);
		Destroy (parObj, 1.5f);
	}

}
