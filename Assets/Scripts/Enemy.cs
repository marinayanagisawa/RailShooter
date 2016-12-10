﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject effect;
	public GameController gc;
	public GameObject player;
	private Vector3 playerPos;
	private Vector3 enemyPos;
	private float moveStartDis = 3.0f;
	public AudioSource sound;
	public GameObject shotPosition;
	public GameObject enemyShot;

	public bool isShot = false;

	public int enemyScore = 10;

	void Start() {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		sound = GetComponent<AudioSource> ();
	}

	void Update(){
		
		playerPos = player.transform.position;
		enemyPos = this.transform.position;

		float dis = Vector3.Distance (playerPos, enemyPos);
		if (dis < moveStartDis) {
			//Debug.Log (gameObject.name + "との距離" + dis);
			if (!isShot) {
				shot ();
				isShot = true;
			}
			//敵の移動をタイプ別に関数にまとめてここで呼ぶ
		}

	}

	void OnTriggerEnter(Collider col){

		sound.PlayOneShot (sound.clip);
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
		Destroy (this.gameObject, 1.5f);
	}


	//移動
	//ショット

	void shot(){

		Instantiate (enemyShot, shotPosition.transform.position, shotPosition.transform.rotation);

	}
	
}
