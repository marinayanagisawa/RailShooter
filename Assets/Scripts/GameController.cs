using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text pauseText;
	public Text startText;

	public bool clear = false;
	public bool isPause = false;
	private GameObject player;
	private PlayerController pc;

	public GameObject playCanvas;
	public GameObject resultCanvas;

	void Start () {

		LocalValues.gameFlg = true;

		playCanvas.SetActive(true);
		resultCanvas.SetActive(false);

		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();

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



		if (pc.hp <= 0) {
			//GameOver表示とリザルト
			pc.GetComponent<NavMeshAgent>().Stop();
			playCanvas.SetActive(false);
			resultCanvas.SetActive(true);
	}
		if (clear) {
			//GameClear表示とリザルト
			pc.GetComponent<NavMeshAgent>().Stop();
			playCanvas.SetActive(false);
			resultCanvas.SetActive(true);
		}
		


	}

	public void addScore(int score) {

		LocalValues.totalScore += score;
		scoreText.text = LocalValues.totalScore.ToString();
		LocalValues.beatNum++;
	
	}

	//リザルト処理
	void Result() {

		//UIなどを出す
	}

}
