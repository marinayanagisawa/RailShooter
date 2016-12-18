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

	public Text centerText;
	public Text title;
	public Text score;
	public Text life;
	public Text beat;
	public Text Shot;
	public Text totalScore;
	public Text hitPer;


	void Start() {
		playCanvas.SetActive(false);
		resultCanvas.SetActive(true);

		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		player.GetComponent<NavMeshAgent>().Stop();
		//centerText = resultCanvas.transform.FindChild("StartText").GetComponent<Text>();
		//title = resultCanvas.transform.FindChild("Title").GetComponent<Text>();
		centerText.text = "START";

		Invoke("GameStart", 2.0f);
	}
	

	void Update () {

		if (LocalValues.gameFlg) {

			//ポーズ
			if (!isPause) {
				if ((Input.GetKeyDown(KeyCode.P)) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
					pauseText.text = "PAUSE";
					Time.timeScale = 0;
					isPause = true;
					Debug.Log("B" + LocalValues.beatNum + " S" + LocalValues.shotNum);
				}
			} else {
				if ((Input.GetKeyDown(KeyCode.P)) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
					pauseText.text = "";
					Time.timeScale = 1;
					isPause = false;
				}
			}



			if (pc.hp <= 0) {
				//GameOver演出、リザルト
				title.text = "FAILURE...";
				Finish();
			}

			if (clear) {
				//GameClear表示とリザルト
				title.text = "CLEAR";
				Finish();
			}

		}

	}

	public void addScore(int score) {

		LocalValues.totalScore += score;
		scoreText.text = LocalValues.totalScore.ToString();
		LocalValues.beatNum++;
	
	}

	void GameStart() {
		
		LocalValues.gameFlg = true;
		centerText.text = "";
		resultCanvas.SetActive(false);
		playCanvas.SetActive(true);
		player.GetComponent<NavMeshAgent>().Resume();

	}

	//ゲーム終了
	void Finish() {

		LocalValues.gameFlg = false;
		pc.GetComponent<NavMeshAgent>().Stop();
		playCanvas.SetActive(false);
		resultCanvas.SetActive(true);

		StartCoroutine("Result");
	}
	
	IEnumerator Result() {

		yield return new WaitForSeconds(0.5f);
		score.text = LocalValues.totalScore.ToString();

		yield return new WaitForSeconds(0.5f);
		int l = pc.hp * 1000;
		life.text =  l.ToString();

		yield return new WaitForSeconds(0.5f);
		beat.text = LocalValues.beatNum.ToString();

		yield return new WaitForSeconds(0.5f);
		Shot.text = LocalValues.shotNum.ToString();

		yield return new WaitForSeconds(0.5f);
		int t = (LocalValues.totalScore + pc.hp * 1000);
		totalScore.text = t.ToString();

		yield return new WaitForSeconds(0.5f);
		float h = (float)LocalValues.beatNum / (float)LocalValues.shotNum;
		h = (int)h * 100;
		hitPer.text =  h + "%";
	
	
	}
	
}