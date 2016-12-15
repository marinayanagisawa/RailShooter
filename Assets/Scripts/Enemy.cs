using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject effect;
	private GameController gc;
	private GameObject player;
	private Vector3 playerPos;
	private Vector3 enemyPos;
	public float moveStartDis = 7.0f;
	public float shotDis = 4.0f;
	private AudioSource sound;
	public GameObject shotPosition;
	public GameObject enemyShot;
	//public LayerMask layerMask;

	public bool aleadyShot = false;
	private bool enemyDead = false;

	private int enemyScore = 100;

	void Start() {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		player = GameObject.Find ("Player");
		sound = GetComponent<AudioSource> ();
	}

	void Update(){
		
		playerPos = player.transform.position;
		enemyPos = this.transform.position;

		float dis = Vector3.Distance (playerPos, enemyPos);

		if (dis < moveStartDis) {
			this.transform.LookAt (player.transform.position);
		}

		if (dis < shotDis) {
			//Debug.Log (gameObject.name + "との距離" + dis);

			//とりあえず1回撃ってみる
			if (!enemyDead) {
				if (!aleadyShot) {
					Shot();
					aleadyShot = true;
				}
			}
		}


	}

	void OnTriggerEnter(Collider col){
		int hitlayer = col.gameObject.layer;
		if (LayerMask.LayerToName (hitlayer) == "PlayerShot") {
			sound.PlayOneShot (sound.clip);
			transform.FindChild ("Explosion").GetComponent<ParticleSystem> ().Play ();
			DestroyEff (effect);
			gc.addScore (enemyScore);
			EleaseAndDestroy ();
		
		} else {
			//プレイヤーと当たった場合
			EleaseAndDestroy ();
		}
	}
		
	void DestroyEff(GameObject eff){
		Destroy (eff, 0.7f);
	}


	void EleaseAndDestroy(){
		enemyDead = true;
		//GetComponent<MeshRenderer> ().enabled = false;
		transform.FindChild("PA_Warrior").transform.position = new Vector3(-1000.0f,-1000.0f,-1000.0f);
		GetComponent<BoxCollider> ().enabled = false;
		Destroy (this.gameObject, 1.5f);
	}


	//移動
	
	//ショット
	void Shot(){

		Instantiate (enemyShot, shotPosition.transform.position, shotPosition.transform.rotation);

	}
	
}
