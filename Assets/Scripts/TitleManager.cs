using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour {

	public Animator anim;
	public AudioSource sound;

	//メニューが出ているか
	public bool ShowMenu = true;
	private bool isStartOp = false;
	
	private GameObject forcusObj;

	void Start() {
		//ハイライトされているUIを取得
		forcusObj = EventSystem.current.firstSelectedGameObject;
	}

	void Update(){

		if (ShowMenu) {
			
			//比較する
			if (forcusObj != EventSystem.current.currentSelectedGameObject) {
				sound.PlayOneShot(sound.clip);
			}

			//lastSelectedObjに代入
			forcusObj = EventSystem.current.currentSelectedGameObject;


			if (!isStartOp) {
				Invoke("StartOp", 15.0f);
				isStartOp = true;
			}


		} else {
			if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
				FinishOp();
				ShowMenu = true;
				
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
		ShowMenu = false;

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
