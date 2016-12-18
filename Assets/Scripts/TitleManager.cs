using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		


	}


	public void NormalCheck() {
		LocalValues.reversal = false;
	}

	public void ReversalCheck() {
		LocalValues.reversal = true;
	}

	public void GameStart() {
		SceneManager.LoadScene("Stage1");
	}
}
