using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text pauseText;
	public Text startText;

	public bool isPause = false;


	void Start () {

		LocalValues.gameFlg = true;

	}
	

	void Update () {

		//ポーズ
		if (!isPause) {
			if ((Input.GetKeyDown(KeyCode.P)) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
				pauseText.text = "PAUSE";
				Time.timeScale = 0;
				isPause = true;
				Debug.Log ("B"+LocalValues.beatNum +" S"+LocalValues.shotNum);
			}
		} else {
			if ((Input.GetKeyDown(KeyCode.P)) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
				pauseText.text = "";
				Time.timeScale = 1;
				isPause = false;
			}
		}


	}

	public void addScore(int score) {

		LocalValues.totalScore += score;
		scoreText.text = "SCORE" + LocalValues.totalScore;
		LocalValues.beatNum++;
	
	}

	//クリアリザルト処理
	

}
