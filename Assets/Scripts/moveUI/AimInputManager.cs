using UnityEngine;
using System.Collections;

/// <summary>////////////////////
/// 照準をキーで動かして,照準の方向を銃口が追いかけるタイプ
/// 
/// ワールド座標内の空オブジェクト（aim）を方向キーで直接操作し
/// UIの照準(Target.cs)と銃口(GunDirection.cs)がオブジェクトを追いかける
/// P/////////////////////////// </summary>

public class AimInputManager : MonoBehaviour {

	private float moveSpeed =0.15f;
	private float inputX;
	private float inputY;

	public GameObject shot;
	public GameObject mazzle;
	public Transform aim;

	//スティック用反転入力オン
	public bool reversal= true;

	private Vector3 pos;
	private Vector3 aimPos;
	private Vector3 currentPos;

	void Update () {

		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");

		//aimオブジェクトが画面内なら,現在地を記憶して移動,更に移動直後に画面外に出たら,同フレーム内で記憶していた場所に戻る
		if (ViewPointCheck()) {

			currentPos = aim.position;
			MoveTarget ();

			if (!ViewPointCheck ()) {
				aim.position = currentPos;
			}
				
			//aimオブジェクトが画面外に出たら,前フレームで記憶していた場所に戻る
		} else {
			aim.position = currentPos;
		}


		//発射！
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (shot, mazzle.transform.position, transform.rotation);
		}


	}

	//aimオブジェクトが画面内にいたらTrueが返る
	bool ViewPointCheck(){
		aimPos = Camera.main.WorldToViewportPoint(aim.position);
		if (aimPos.x > 0 && aimPos.x < 1 && aimPos.y > 0 && aimPos.y < 1) {
			return true;
		} else {
			return false;
		}
	}


	void MoveTarget() {
		if (!reversal) {
			pos = new Vector3(transform.position.x + (inputX * moveSpeed), transform.position.y + (inputY * moveSpeed), transform.position.z);
		} else {
			pos = new Vector3(transform.position.x + (inputX * -moveSpeed), transform.position.y + (inputY * -moveSpeed), transform.position.z);
		
		
		
		}
		this.transform.position = pos;
	}


}
