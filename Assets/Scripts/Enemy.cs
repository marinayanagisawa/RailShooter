using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject effect;
	public GameController gc;
	public GameObject player;
	private Vector3 playerPos;
	private Vector3 enemyPos;
	private float moveStartDis = 7.0f;
	private float shotDis = 4.0f;
	public AudioSource sound;
	public GameObject shotPosition;
	public GameObject enemyShot;
	public LayerMask layerMask;

	public bool aleadyShot = false;
	private bool enemyDead = false;

	private int enemyScore = 100;

	void Start() {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		sound = GetComponent<AudioSource> ();
	}

	void Update(){
		
		playerPos = player.transform.position;
		enemyPos = this.transform.position;

		float dis = Vector3.Distance (playerPos, enemyPos);

		if (dis < moveStartDis) {
			
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

			sound.PlayOneShot(sound.clip);
			transform.FindChild("Explosion").GetComponent<ParticleSystem>().Play();
			DestroyEff(effect);
			gc.addScore(enemyScore);
			EleaseAndDestroy();
		
	}
		
	void DestroyEff(GameObject eff){
		Destroy (eff, 0.7f);
	}


	void EleaseAndDestroy(){
		enemyDead = true;
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;
		Destroy (this.gameObject, 1.5f);
	}


	//移動
	
	//ショット
	void Shot(){

		Instantiate (enemyShot, shotPosition.transform.position, shotPosition.transform.rotation);

	}
	
}
