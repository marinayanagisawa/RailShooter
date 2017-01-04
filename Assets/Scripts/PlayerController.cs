using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public int hp;
	public Slider slider;
	private AudioSource hitSound;
	public GameObject cam;
	private Vector3 cameraPos;

	public GameObject hpBack;
	public GameObject hpFore;
	public GameObject hpText;

	private GameController gc;

	void Start () {

		cameraPos = Camera.main.transform.localPosition;
		
		slider.GetComponent<Slider>();
		hp = 3;
	
		hitSound = GetComponent<AudioSource> ();

		gc = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	void Update () {
		//HPを表示
		slider.value = hp;

		if (hp < 2) {
			hpBack.GetComponent<Image> ().color = new Color(1f, 0.0f, 0.0f, 0.5f);
			hpFore.GetComponent<Image>().color = new Color(1f, 0.0f, 0.0f, 0.5f);
			hpText.GetComponent<Image>().color = new Color(1f, 0.0f, 0.0f, 0.5f);
		}
			
		//ステージ作成時のテスト用
		//RotateTest ();

	}


	void OnTriggerEnter(Collider col) {
		string layerMask = LayerMask.LayerToName (col.gameObject.layer);

		//if ((layerMask == "Enemy")||(layerMask == "EnemyShot")){
		if (layerMask == "EnemyShot") {
			
			//enemyShotとヒットしたときの処理(音,画面揺れ,HP計算)
			hitSound.PlayOneShot (hitSound.clip);
			iTween.ShakePosition (cam, iTween.Hash ("x", 0.2f, "y", 0.2f, "time", 0.4f));

			//カメラ揺れ後の位置を調整
			Invoke ("CamPosCheck", 0.6f);

			if (hp > 0) {
				hp--;
			}

		} else if (layerMask == "GoalArea") {

			//gameFlgはGameControllerのFinish関数内で変更
			gc.clear = true;
		}

	}
		
	void CamPosCheck(){
		Debug.Log ("カメラ位置修正");
		Vector3 afterHitPos = Camera.main.transform.localPosition;
		if (cameraPos != afterHitPos) {
			Camera.main.transform.localPosition = cameraPos;
		}

	}


	public void CameraTurn(int euler){
		//Y軸のプラス（マイナス）に進む時は,0（180）で正面,90（270）で右向き,270（90）で左向き
		//X軸のプラス（マイナス）に進む時は,90（270）で正面,180（0）で右向き,0（180）で左向き
		iTween.RotateTo (cam, iTween.Hash ("y", euler, "time", 3.0f));
	}

	public void CameraTurnX(int euler){
		iTween.RotateTo (cam, iTween.Hash ("x", euler, "time", 2.5f));
	}

	//検証用の方向指示（テスト専用）
	#if UNITY_EDITOR

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

	}
	#endif
}
