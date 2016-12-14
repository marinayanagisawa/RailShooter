using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public int hp;
	public Slider slider;
	private AudioSource hitSound;
	public GameObject cam;

	void Start () {
		//--------------ゲームフラグを確認してから移動スタートさせる

		slider.GetComponent<Slider>();
		hp = 3;
	
		hitSound = GetComponent<AudioSource> ();

	}
	
	void Update () {
		//HPを表示
		slider.value = hp;

		//ステージ作成時のテスト用
		RotateTest ();

	}


	void OnTriggerEnter(Collider col) {

		//enemyShotとヒットしたときの処理(音,画面揺れ,HP計算)
		hitSound.PlayOneShot(hitSound.clip);
		iTween.ShakePosition (cam, iTween.Hash ("x", 0.2f, "y", 0.2f, "time", 0.4f));

		if (hp > 0) {
			hp--;
		}
		
	}
		

	public void CameraTurn(int euler){
		//Y軸のプラス（マイナス）に進む時は,0（180）で正面,90（270）で右向き,270（90）で左向き
		//X軸のプラス（マイナス）に進む時は,90（270）で正面,180（0）で右向き,0（180）で左向き
		iTween.RotateTo (cam, iTween.Hash ("y", euler, "time", 3.0f));
	}

	//検証用の方向指示（テスト専用）
	void RotateTest(){
		if (Input.GetKeyDown (KeyCode.Keypad8)) {
			CameraTurn (0);
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			CameraTurn (180);
		}
		if (Input.GetKeyDown (KeyCode.Keypad6)) {
			CameraTurn (90);
		}
		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			CameraTurn (270);
		}
		if (Input.GetKeyDown (KeyCode.Keypad9)) {
			CameraTurn (45);
		}
		if (Input.GetKeyDown (KeyCode.Keypad3)) {
			CameraTurn (135);
		}
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			CameraTurn (225);
		}
		if (Input.GetKeyDown (KeyCode.Keypad7)) {
			CameraTurn (315);
		}
	}

}
