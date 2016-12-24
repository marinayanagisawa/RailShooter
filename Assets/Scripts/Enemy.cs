using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public bool aleadyShot = false;
	public bool walk = false;
	public bool fly = false;
	private Animator moveAnim;

	private bool enemyDead = false;

	public int enemyScore = 100;


	void Start() {
		moveAnim = GetComponent<Animator> ();
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		player = GameObject.Find ("Player");
		sound = GetComponent<AudioSource> ();
	}

	void Update(){

		//距離を測って活動
		playerPos = player.transform.position;
		enemyPos = this.transform.position;

		float dis = Vector3.Distance (playerPos, enemyPos);


		if (walk) {
			if (dis < moveStartDis) {
				//前方に移動
				moveAnim.SetTrigger("Move");
			}
		}

		if (fly) {
			if (dis < moveStartDis) {
				moveAnim.SetTrigger ("Fly");
			}
		}

		if (dis < shotDis) {
			//Debug.Log (gameObject.name + "との距離" + dis);

			//射程に入ったら,1回撃つ
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

		//プレイヤーの弾と衝突した処理（爆発音,パーティクルの呼び出しと消去,スコア加算,オブジェクト消去）
		if (LayerMask.LayerToName (hitlayer) == "PlayerShot") {
			sound.PlayOneShot (sound.clip);
			transform.FindChild ("Explosion").GetComponent<ParticleSystem> ().Play ();
			DestroyEff (effect);
			gc.addScore (enemyScore);
			EleaseAndDestroy ();
		
		} else if(LayerMask.LayerToName (hitlayer) == "Player") {
			//プレイヤーと当たった場合は消える
			EleaseAndDestroy ();
		}
	}
		
	//爆発
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


	//ショット
	void Shot(){
		Instantiate (enemyShot, shotPosition.transform.position, shotPosition.transform.rotation);
	}

	//アニメーションから使用
	void Destroy(){
		Destroy (this.gameObject);
	}
	
}
