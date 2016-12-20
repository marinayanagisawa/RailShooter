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

	private Animator panelAnim;

	public Text centerText;
	public Text title;
	public Text score;
	public Text life;
	public Text beat;
	public Text Shot;
	public Text totalScore;
	public Text hitPer;

	void Start() {

		panelAnim = resultCanvas.transform.FindChild ("Panel").GetComponent<Animator> ();

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

		panelAnim.SetTrigger ("mFadeOut");
		StartCoroutine("Result");
	}
	
	IEnumerator Result() {

		score.text = LocalValues.totalScore.ToString();
		int l = pc.hp * 1000;
		life.text =  l.ToString();
		beat.text = LocalValues.beatNum.ToString();
		Shot.text = LocalValues.shotNum.ToString();

		int t = (LocalValues.totalScore + pc.hp * 1000);
		totalScore.text = t.ToString();

		//一度も撃たないと変な数字が代入されるため
		if (LocalValues.shotNum == 0) {
			hitPer.text = "0%";
		} else {
			float h = ((float)LocalValues.beatNum / (float)LocalValues.shotNum) * 100;
			h = (int)h;
			hitPer.text = h + "%";
		}
	
		yield return new WaitForSeconds(1.0f);
		panelAnim.SetTrigger ("result");

	}


}