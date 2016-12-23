using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour {

	public Animator anim;

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
}
