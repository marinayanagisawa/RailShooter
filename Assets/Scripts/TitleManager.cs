using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour {

	public Animator anim;
	public AudioSource audio;

	//public EventSystem ev;

	void Update(){


		if (Input.GetAxis ("Horizontal") != 0  || Input.GetAxis ("Vertical") != 0) {
			audio.PlayOneShot (audio.clip);

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
