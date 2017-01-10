using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public Text hitRate;
	public string rank;

	private int rankNum;

	//ランク表示用の画像
	private Sprite[] rankImages = new Sprite[6];
	//ランク表示のUI(resultCanvas内)を紐づけ
	public Image rankIcon;

	void Start() {
		LocalValues.Init ();

		//ランク表示に使うイメージを取得
		rankImages = Resources.LoadAll<Sprite> ("Image/");

		panelAnim = resultCanvas.transform.FindChild ("Panel").GetComponent<Animator> ();

		playCanvas.SetActive(false);
		resultCanvas.SetActive(true);

		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();

		//スタート表示の間はNavMeshを止めておく
		player.GetComponent<NavMeshAgent>().Stop();
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
				title.GetComponent<Text>().color = new Color(1f, 0.0f, 0.0f, 0.5f);
				title.text = "FAILURE...";
				Finish();
			}

			if (clear) {
				//GameClear表示とリザルト
				title.text = "COMPLETE";
				Finish();
			}

		}

	}

	public void addScore(int score) {

		LocalValues.score += score;
		scoreText.text = LocalValues.score.ToString();
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

		score.text = LocalValues.score.ToString();
		int l = pc.hp * 1000;
		life.text =  l.ToString();
		beat.text = LocalValues.beatNum.ToString();
		Shot.text = LocalValues.shotNum.ToString();

		int t = (LocalValues.score + pc.hp * 1000);
		totalScore.text = t.ToString();
		LocalValues.totalScore = t;

		//一度も撃たない時は0%
		if (LocalValues.shotNum == 0) {
			hitRate.text = "0%";
			LocalValues.hitRate = 0;
		} else {
			float h = ((float)LocalValues.beatNum / (float)LocalValues.shotNum) * 100;
			int hRate = (int)h;
			hitRate.text = hRate + " %";
			LocalValues.hitRate = hRate;
		}
	
		//腕前ランクを判断して表示
		RankCheck ();
		rankIcon.sprite = rankImages[rankNum];

		yield return new WaitForSeconds(1.0f);
		panelAnim.SetTrigger ("result");

		yield return new WaitForSeconds (10.0f);
		//シーン移動
		SceneManager.LoadScene("ScoreRanking");
	}


	//腕前チェック（条件は暫定, ステージが完成してから調整）
	void RankCheck(){
		if (LocalValues.totalScore <= 8000) {
			rankNum = 3;
			rank = "D";
		}
		if (LocalValues.totalScore > 8000 && LocalValues.totalScore < 13000) {
			rankNum = 2;
			rank = "C";
		}
		if (LocalValues.totalScore > 13000 && LocalValues.totalScore < 17000) {
			rankNum = 1;
			rank = "B";
		}
		if (LocalValues.totalScore >= 17000) {
			rankNum = 0;
			rank = "A";
		}
		if (LocalValues.totalScore >= 20000 && LocalValues.hitRate > 79) {
			rankNum = 5;
			rank = "S";
		} 
		if (pc.hp <= 0) {
			rankNum = 4;
			rank = "E";
		}

		LocalValues.rank = rank;
	}

}