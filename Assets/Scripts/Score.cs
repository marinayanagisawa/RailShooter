using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// スコアランキングのスクリプト
/// ranking配列には,　スコア,　評価（int）,　命中率 の順で結合したstringを格納
/// </summary>

public class Score : MonoBehaviour {
	
	//文字列結合してランキングデータを入れる(最後の１つは今回のスコア用に確保)
	private string[] ranking = new string[6];
	private int[] lastRankingScore = new int[5];

	//playerPrefsのためのキーの文字列
	private string[] scoreKey = new string[5];

	//Canvasから紐づけ
	public Text[] score = new Text[5];
	public Text[] hitRate = new Text[5];
	public Text[] rank = new Text[5];
	public Text currentScore;

	void Start () {

		//キーのための文字列を作成
		for (int i = 0; i < 5; i++) {
			scoreKey [i] = "score" + i;
		}

        //rankingに保存データを取り出し（String）
		for (int i = 0; i < 5; i++) {
			ranking[i] = PlayerPrefs.GetString(scoreKey[i],"0,E,0");
			Debug.Log(ranking[i]);
		}
			
		//前回保存されたスコアを,文字列からintに直して取っておく
		for (int i = 0; i < 5; i++) {
			//lastRankingScore[i] = rankingScore[i];
			lastRankingScore[i] = int.Parse(ranking[i].Split(',')[0]);
		}

		//カンマ区切りで今回のスコアと評価を配列の最後に追加
		ranking[5] = LocalValues.totalScore + "," + LocalValues.rank + "," +LocalValues.hitRate;

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
				
				if (int.Parse(ranking[i].Split(',')[0]) != lastRankingScore[i]) {
					rankingNum = i;
					Debug.Log ((rankingNum+1) + "位にランクイン！");
					changeRank = true;
				}
			}
		}
			

		//ランキング順位に変更があったら文字色を変える
		if (changeRank) {
			score [rankingNum].GetComponent<Text> ().color = Color.red;
			rank [rankingNum].GetComponent<Text> ().color = Color.red;
			hitRate [rankingNum].GetComponent<Text> ().color = Color.red;
			score [rankingNum].transform.parent.GetComponent<Image> ().color = Color.red;  //枠
		}
			
		//スコアを表示
		score [0].text = "1st   " + ranking [0].Split (',') [0];
		score [1].text = "2nd   " + ranking [1].Split (',') [0];
		score [2].text = "3rd   " + ranking [2].Split (',') [0];
		score [3].text = "4th   " + ranking [3].Split (',') [0];
		score [4].text = "5th   " + ranking [4].Split (',') [0];

		//命中率を表示
		for (int i = 0; i < 5; i++) {
			hitRate [i].text = ranking [i].Split (',') [2] + "%";
		}

		//評価を表示
		for (int i = 0; i < 5; i++) {
			rank [i].text = ranking [i].Split (',') [1];
		}


		//1位だったら赤字で表示
		if (rankingNum == 0 && changeRank) {
			currentScore.GetComponent<Text> ().color = Color.red;
			currentScore.text = "You Got HighScore!!  " + LocalValues.totalScore; 
		} else {
			currentScore.text = "Your Score  " + LocalValues.totalScore; 
		}

		//配列の中身をplayerPrefsに保存する
		for (int i = 0; i < 5; i++) {
			
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
