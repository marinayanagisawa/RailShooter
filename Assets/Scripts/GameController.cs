using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool gameFlg;
	public Text scoreText;
	public Text pauseText;

	public int totalScore;
	public int beatNum = 0;
	public bool isPause = false;


	void Start () {

		gameFlg = true;

	}
	

	void Update () {

		//ポーズ
		if (!isPause) {
			if (Input.GetKeyDown(KeyCode.S)) {
				pauseText.text = "PAUSE";
				Time.timeScale = 0;
				isPause = true;
			}
		} else {
			if (Input.GetKeyDown(KeyCode.S)) {
				pauseText.text = "";
				Time.timeScale = 1;
				isPause = false;
			}
		}


	}

	public void addScore(int score) {

		totalScore += score;
		scoreText.text = "Score" + totalScore;
		beatNum++;
	}

	//クリアリザルト処理
	

}
