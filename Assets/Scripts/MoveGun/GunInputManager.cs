using UnityEngine;
using System.Collections;

/// <summary>
/// テスト用スクリプト
/// 
/// 方向入力によって銃口の方向を動かし,
/// その延長上のオブジェクトの位置に合わせてUIで照準を出すタイプ
/// 
/// 入力をやめた時点で照準はニュートラルに戻る
/// 
/// 画面四隅は狙えない
/// I///////////////////////////////////// </summary>

public class GunInputManager : MonoBehaviour {

	private float horizontalAngle;
	private float verticalAngle;
	private float hRange = 35.0f;
	private float vRange = 30.0f;

	public GameObject mazzle;
	public GameObject shot;

	//スティック用反転入力オン
	public bool reversal = true;

	void Start () {
		this.transform.localRotation = Quaternion.Euler (0, 0, 0);
	}

	void Update () {

			horizontalAngle = Input.GetAxis ("Horizontal");
			verticalAngle = Input.GetAxis ("Vertical");
			
			if (reversal) {
				this.transform.localRotation = Quaternion.Euler (verticalAngle * vRange, -horizontalAngle * hRange, 0);
			} else {
				this.transform.localRotation = Quaternion.Euler (-verticalAngle * vRange, horizontalAngle * hRange, 0);
			}


		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (shot, mazzle.transform.position, transform.rotation);
		}
			

	}
}
