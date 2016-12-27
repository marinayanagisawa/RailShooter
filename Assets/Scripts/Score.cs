using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	//ここに取り出したスコアを入れる(最後の１つは今回のスコア用に確保)
	private int[] rankingScore = new int[6];

	//playerPrefsのためのキーの文字列
	private string[] scoreKey = new string[5];

	public Text[] rank = new Text[5];
	public Text lastScore;

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

		//今回のスコアを配列の最後に追加
		rankingScore [5] = LocalValues.Score;


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
			
		for (int i = 0; i < 6; i++) {
			Debug.Log ("key番号" + i +";" +rankingScore [i]);
		}
			
		//rankingScoreの上位5件までと,今回のスコアをUI表示する
		rank[0].text = "1st   " + rankingScore[0];
		rank[1].text = "2nd   " + rankingScore[1];
		rank[2].text = "3rd   " + rankingScore[2];
		rank[3].text = "4th   " + rankingScore[3];
		rank[4].text = "5th   " + rankingScore[4];

		lastScore.text = "Your Score  " + LocalValues.totalScore; 

		//配列の中身をplayerPrefsに保存する
		for (int i = 0; i < 5; i++) {
			PlayerPrefs.SetInt(scoreKey[i], rankingScore[i] );
			PlayerPrefs.Save ();
		}
	}

}
