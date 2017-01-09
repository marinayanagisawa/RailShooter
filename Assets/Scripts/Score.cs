﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

	//ここに取り出したスコアを入れる(最後の１つは今回のスコア用に確保)
	//private int[] rankingScore = new int[6];

	
	//文字列結合してランキングデータを入れる(最後の１つは今回のスコア用に確保)
	private string[] ranking = new string[6];
	private int[] lastRankingScore = new int[5];

	//playerPrefsのためのキーの文字列
	private string[] scoreKey = new string[5];

	//Canvasから紐づけ
	public Text[] rank = new Text[5];
	public Text currentScore;

	void Start () {

		//キーのための文字列を作成
		for (int i = 0; i < 5; i++) {
			scoreKey [i] = "score" + i;
		}
		/*
		//PlayerPrefsの中身を配列に取り出す. 入っていなければ０が返る
		for (int i = 0; i < 5; i++) {
			rankingScore [i] = PlayerPrefs.GetInt (scoreKey[i],0);
			Debug.Log (rankingScore[i]);
		}
*/

        //rankingに保存データを取り出し（String）
		for (int i = 0; i < 5; i++) {
			ranking[i] = PlayerPrefs.GetString(scoreKey[i],"0,0");
			Debug.Log(ranking[i]);
		}



		//前回保存されたスコアを,文字列からintに直して取っておく
		for (int i = 0; i < 5; i++) {
			//lastRankingScore[i] = rankingScore[i];
			lastRankingScore[i] = int.Parse(ranking[i].Split(',')[0]);
		}

		//今回のスコアを配列の最後に追加
		//rankingScore [5] = LocalValues.totalScore;

		//カンマ区切りで今回のスコアと評価を配列の最後に追加
		ranking[5] = LocalValues.totalScore + "," + LocalValues.rankNum;



		//ranking内を降順でソート（文字列からintに直しつつ比較,文字列のままソート）
		for (int i = 0; i < ranking.Length - 1; i++) {

			for(int j = ranking.Length-1; j >i; j--){

				if (int.Parse(ranking[j].Split(',')[0]) > int.Parse(ranking[j-1].Split(',')[0])) {

					string temp = ranking [j];
					ranking [j] = ranking [j - 1];
					ranking [j - 1] = temp;
				}
			}
		}

		//前回と今回のランキングを比較
		int rankingNum = 0;
		bool changeRank = false;
		for (int i = 0; i < 5; i++) {
			//順位の変更を見つけたらrankingNumに順位を入れる
			if (!changeRank) {
				//if (rankingScore [i] != lastRankingScore [i]) {
				if (int.Parse(ranking[i].Split(',')[0]) != lastRankingScore[i]) {
					rankingNum = i;
					Debug.Log ((rankingNum+1) + "位にランクイン！");
					changeRank = true;
				}
			}
		}

		/*
		//確認用
		for (int i = 0; i < 6; i++) {
			Debug.Log ("key番号" + i +";" +rankingScore [i]);
		}
		*/

		//ランキング順位に変更があったら文字色を変える
		if (changeRank) {
			rank [rankingNum].GetComponent<Text> ().color = Color.red;
		}

		//rankingScoreの上位5件までと,今回のスコアをUI表示する
	/*	rank[0].text = "1st   " + rankingScore[0];
		rank[1].text = "2nd   " + rankingScore[1];
		rank[2].text = "3rd   " + rankingScore[2];
		rank[3].text = "4th   " + rankingScore[3];
		rank[4].text = "5th   " + rankingScore[4];
*/
		rank[0].text = "1st   " + ranking[0].Split(',')[0] + "  " + LocalValues.rank;
		rank[1].text = "2nd   " + ranking[1].Split(',')[0] + "  " + LocalValues.rank;
		rank[2].text = "3rd   " + ranking[2].Split(',')[0] + "  " + LocalValues.rank;
		rank[3].text = "4th   " + ranking[3].Split(',')[0] + "  " + LocalValues.rank;
		rank[4].text = "5th   " + ranking[4].Split(',')[0] + "  " + LocalValues.rank;



		//1位だったら赤字で表示
		if (rankingNum == 0 && changeRank) {
			currentScore.GetComponent<Text> ().color = Color.red;
			currentScore.text = "You Got HighScore!!  " + LocalValues.totalScore; 
		} else {
			currentScore.text = "Your Score  " + LocalValues.totalScore; 
		}

		//配列の中身をplayerPrefsに保存する
		for (int i = 0; i < 5; i++) {
			//PlayerPrefs.SetInt(scoreKey[i], rankingScore[i] );
			PlayerPrefs.SetString(scoreKey[i], ranking[i]);
			PlayerPrefs.Save ();
		}
	}


	void Update(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene ("Stage1");
		}
	}

}
