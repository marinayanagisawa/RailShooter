using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject effect;
	public GameController gc;
	public GameObject player;
	private Vector3 playerPos;
	private Vector3 enemyPos;
	public float moveStartDis = 7.0f;

	public int enemyScore = 10;

	void Start() {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void Update(){
		
		playerPos = player.transform.position;
		enemyPos = this.transform.position;

		float dis = Vector3.Distance (playerPos, enemyPos);
		if (dis < moveStartDis) {
			//Debug.Log (gameObject.name + "との距離" + dis);

			//敵の移動をタイプ別に関数にまとめてここで呼ぶ
		}

	}

	void OnTriggerEnter(Collider col){
		
		transform.FindChild("Explosion").GetComponent<ParticleSystem> ().Play();
		DestroyEff (effect);
		gc.addScore(enemyScore);
		EleaseAndDestroy ();

	}
		
	void DestroyEff(GameObject eff){
		Destroy (eff, 0.7f);
	}


	void EleaseAndDestroy(){
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;
		Destroy (this.gameObject, 1.0f);
	}


	//移動
	//ショット
	
}
