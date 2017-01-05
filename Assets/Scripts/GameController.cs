﻿using UnityEngine;
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

	private int rankNum;

	//ランク表示用の画像
	private Sprite[] rankImages = new Sprite[6];
	//ランク表示のUI(resultCanvas内)を紐づけ
	public Image rankIcon;

	void Start() {
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

		LocalValues.Score += score;
		scoreText.text = LocalValues.Score.ToString();
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

		score.text = LocalValues.Score.ToString();
		int l = pc.hp * 1000;
		life.text =  l.ToString();
		beat.text = LocalValues.beatNum.ToString();
		Shot.text = LocalValues.shotNum.ToString();

		int t = (LocalValues.Score + pc.hp * 1000);
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

		yield return new WaitForSeconds (12.0f);
		//シーン移動
		SceneManager.LoadScene("ScoreRanking");
	}


	//腕前チェック（条件は暫定, ステージが完成してから調整）
	void RankCheck(){
		if (LocalValues.totalScore < 5000) {
			rankNum = 3;  //D
		}
		if (LocalValues.totalScore > 5100 && LocalValues.totalScore < 6900) {
			rankNum = 2;  //C
		}
		if (LocalValues.totalScore > 7000 && LocalValues.totalScore < 9900) {
			rankNum = 1;  //B
		}
		if (LocalValues.totalScore > 10000) {
			rankNum = 0;  //A
		}
		if (LocalValues.totalScore > 12000 && LocalValues.hitRate > 80) {
			rankNum = 5;  //S
		} 
		if (pc.hp <= 0) {
			rankNum = 4;  //E
		}
	}

}