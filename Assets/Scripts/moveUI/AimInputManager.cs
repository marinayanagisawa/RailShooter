using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>////////////////////
/// 照準をキーで動かして,照準の方向を銃口が追いかけるタイプ
/// 
/// ワールド座標内の空オブジェクト（aim）を方向キーで直接操作し
/// UIの照準(Target.cs)と銃口(GunDirection.cs)がオブジェクトを追いかける
/// P/////////////////////////// </summary>

public class AimInputManager : MonoBehaviour {

	private float moveSpeed =0.18f;
	private float inputX;
	private float inputY;

	private GameController gc;
	public GameObject shot;
	public GameObject mazzle;
	public Transform aim;

	//スティック用反転入力オン
	public bool reversal= true;

	private Vector3 pos;
	private Vector3 aimPos;
	private Vector3 currentPos;
	private Vector3 startPos;

	private int shotLeft = 5;
	private bool canShot;

	public AudioSource[] sounds;
	public Image[] shotImage; 

	void Start() {
		canShot = true;
		startPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z); 
		sounds = GetComponents<AudioSource>();
		gc = GameObject.Find("GameController").GetComponent<GameController>();

	}

	void Update () {

		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");

		//Aimオブジェクトをカメラを中心に移動させるため
		transform.LookAt(Camera.main.transform);

		//残弾表示
		for (int i = 0; i < 6; i++) {
			shotImage [i].GetComponent<Image> ().enabled = false;
		}

		shotImage[shotLeft].GetComponent<Image>().enabled = true;

		if (!gc.isPause) {
		//移動処理	
		//aimオブジェクトが画面内なら,現在地を記憶して移動,更に移動直後に画面外に出たら,同フレーム内で記憶していた場所に戻る
		if (ViewPointCheck()) {

				currentPos = aim.position;
				MoveTarget();

				if (!ViewPointCheck()) {
					aim.position = currentPos;

				}
				//aimオブジェクトが画面外に出たら,前フレームで記憶していた場所に戻る
			} else {
				aim.position = currentPos;
			}


			//発射！
			if (shotLeft > 0 && canShot) {
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
					Instantiate(shot, mazzle.transform.position, transform.rotation);
					sounds[0].PlayOneShot(sounds[0].clip);
					shotLeft--;
				}
			}

			if (shotLeft < 0) {
				//空振り音
				//sounds[2].PlayOneShot(sounds[2].clip);
			}

			//リロード
			if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Return)) {
				if (shotLeft < 5) {
					sounds[2].PlayOneShot(sounds[2].clip);
					StartCoroutine("Reload");
				}
			}

			//照準のリセット
			if (Input.GetKey(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.LeftShift)) {

				moveSpeed = 0.05f;

				if (inputX == 0 && inputY == 0) {
					transform.localPosition = startPos;
				}
			}

			if (Input.GetKeyUp(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.LeftShift)) {
				moveSpeed = 0.18f;
			}
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
			transform.localPosition = new Vector3(transform.localPosition.x + (inputX * moveSpeed), transform.localPosition.y + (inputY * moveSpeed), transform.localPosition.z);
		} else {
			transform.localPosition = new Vector3(transform.localPosition.x + (inputX * -moveSpeed), transform.localPosition.y + (inputY * -moveSpeed), transform.localPosition.z);

		}
	}


	IEnumerator Reload() {
		
		canShot = false;
		yield return new WaitForSeconds(1.5f);
		shotLeft = 5;
		canShot = true;
	}

}
