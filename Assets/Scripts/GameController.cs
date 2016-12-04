using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool gameFlg;
	public Text scoreText;
	public int totalScore;
	public int beatNum = 0;


	void Start () {

		gameFlg = true;

	}
	

	void Update () {
	
	}

	public void addScore(int score) {

		totalScore += score;
		scoreText.text = "Score" + totalScore;
		beatNum++;
	}

	//クリアリザルト処理
	

}
