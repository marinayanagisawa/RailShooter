using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour {

	public Animator anim;
	public AudioSource sound;
	public bool isShowMenu = true;
	
	private GameObject forcusObj;

	void Start() {
		//ハイライトされているUIを取得
		forcusObj = EventSystem.current.firstSelectedGameObject;
	}

	void Update(){

		if (isShowMenu) {
			

			//比較する
			if (forcusObj != EventSystem.current.currentSelectedGameObject) {
			sound.PlayOneShot(sound.clip);
		}

		//lastSelectedObjに代入
		forcusObj = EventSystem.current.currentSelectedGameObject;


		Invoke("StartOp", 15.0f);
		

		} else {
			if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
				FinishOp();
				isShowMenu = true;
				
			}

		}

	}

	public void NormalCheck() {
		LocalValues.reversal = false;
	}

	public void ReversalCheck() {
		LocalValues.reversal = true;
	}

	public void GameStart() {

		anim.SetTrigger ("MenuOut");
		Invoke("MoveScene",1.5f);
	}

	public void StartOp() {
		anim.SetTrigger("StartOp");
		isShowMenu = false;

	}

	public void FinishOp() {
		anim.SetTrigger("FinishOp");
		
	}

	public void MoveScene(){
		SceneManager.LoadScene("Stage1");

	}

	#if UNITY_EDITOR
	//Debug用ボタン
	void OnGUI(){
		if (GUI.Button(new Rect(0,0,100,50),"Reset")){
			//保存データを初期化
			PlayerPrefs.DeleteAll();
			Debug.Log ("HighScore Reset!");
		}
	}
	#endif
}
