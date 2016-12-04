using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int hp = 3;
	GameObject enemyShot;


	void Start () {
		//ゲームフラグを確認してから移動スタート
	
	}
	
	void Update () {
	
	}


	void OnTriggerEnter(Collider col) {

		//enemyShotとヒットしたときの処理（hp--,hp残り判定,音、エフェクト）
		//ただし、弾が飛ぶ、画面揺れなどは単に演出にしてHPを引く場合はこのメソッド自体いらない
	}


	//プレイヤーのカメラ回転メソッド（必要に応じて）

}
