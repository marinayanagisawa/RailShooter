using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	//ここに取り出したスコアを入れる(最後の１つは今回のスコア用に確保)
	private int[] rankingScore = new int[6];
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
			Debug.Log (scoreKey[i]);
		}

		//PlayerPrefsの中身を配列に取り出す. 入っていなければ０が返る
		for (int i = 0; i < 5; i++) {
			rankingScore [i] = PlayerPrefs.GetInt (scoreKey[i],0);
			Debug.Log (rankingScore[i]);
		}

		//前回保存されたランキングを取っておく
		for (int i = 0; i < 5; i++) {
			lastRankingScore[i] = rankingScore[i];
		}

		//今回のスコアを配列の最後に追加
		rankingScore [5] = LocalValues.totalScore;


		//rankingScore内を降順でソート
		for (int i = 0; i < rankingScore.Length - 1; i++) {

			for(int j = rankingScore.Length-1; j >i; j--){

				if (rankingScore [j] > rankingScore [j - 1]) {
					int temp = rankingScore [j];
					rankingScore [j] = rankingScore [j - 1];
					rankingScore [j - 1] = temp;
				}
			}

		}

		//前回と今回のランキングを比較
		int rankNum = 0;
		bool changeRank = false;
		for (int i = 0; i < 5; i++) {
			//順位の変更を見つけたらrankNumに順位を入れて,以降は無視
			if (!changeRank) {
				if (rankingScore [i] != lastRankingScore [i]) {
					rankNum = i;
					Debug.Log ((rankNum+1) + "位にランクイン！");
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
			rank [rankNum].GetComponent<Text> ().color = Color.red;
		}

		//rankingScoreの上位5件までと,今回のスコアをUI表示する
		rank[0].text = "1st   " + rankingScore[0];
		rank[1].text = "2nd   " + rankingScore[1];
		rank[2].text = "3rd   " + rankingScore[2];
		rank[3].text = "4th   " + rankingScore[3];
		rank[4].text = "5th   " + rankingScore[4];

		if (rankNum == 0 && changeRank) {
			currentScore.GetComponent<Text> ().color = Color.red;
			currentScore.text = "You Got HighScore!!  " + LocalValues.totalScore; 
		} else {
			currentScore.text = "Your Score  " + LocalValues.totalScore; 
		}

		//配列の中身をplayerPrefsに保存する
		for (int i = 0; i < 5; i++) {
			PlayerPrefs.SetInt(scoreKey[i], rankingScore[i] );
			PlayerPrefs.Save ();
		}
	}

}
