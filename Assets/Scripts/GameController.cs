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

	private string comment;
	private char[] showComment = new char[100];
	//コメント表示欄を紐づけ
	public Text rankCommentText;
	private AudioSource sound;

	//ランク表示用の画像
	private Sprite[] rankImages = new Sprite[6];
	//ランク表示のUI(resultCanvas内)を紐づけ
	public Image rankIcon;

	void Start() {
		LocalValues.Init ();
		sound = GetComponent<AudioSource> ();

		//ランク表示に使うイメージを取得
		rankImages = Resources.LoadAll<Sprite> ("Image/");

		panelAnim = resultCanvas.transform.FindChild ("Panel").GetComponent<Animator> ();

		playCanvas.SetActive(false);
		resultCanvas.SetActive(true);

		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();

		//スタート表示の間はNavMeshを止めておく
		player.GetComponent<NavMeshAgent>().Stop();
		centerText.text = "MISSION ACCEPTED";

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
				title.text = "MISSION FAILURE...";
				Finish();
			}

			if (clear) {
				//GameClear表示とリザルト
				title.text = "MISSION ACCOMPLISHED";
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

		//リザルトアニメーション
		yield return new WaitForSeconds(1.0f);
		panelAnim.SetTrigger ("result");

		//rankNumから表示コメントを決定
		RankComment (rankNum);

		//stringをcharの配列に代入
		showComment = comment.ToCharArray();
		yield return new WaitForSeconds (7.0f);

		//一文字ずつ表示する文字を増やす
		for (int i = 0; i < comment.Length; i++) {
			rankCommentText.text += showComment [i];
			sound.PlayOneShot (sound.clip);
			yield return new WaitForSeconds (0.004f);
		}

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
		

	void RankComment(int rankN){
		if (rankN == 4) {
			comment = "まずはミッションの達成を目指してほしい。\nショットを撃つタイプの敵とドローンは、" +
				"撃たれる前に最優先で破壊せよ。";
		} else if (rankN == 3) {
			comment = "残弾に常に注意して、こまめなリロードを行うことが重要だ。\nまた、リロード中は暫く撃てなくなるので、" +
				"タイミングに注意して行ってほしい。";
		}else if (rankN == 2) {
			comment = "敵によって獲得スコアが違う。動かない敵、狙い易い敵は獲得スコアも低い。\n優先順位を考えて破壊せよ。";
		}else if (rankN == 1) {
			comment = "敵の弾は破壊した時に多くのスコアを獲得できる。わざと撃たせて、落ち着いて撃ち落とすのも手だ。\n" +
				"また、無駄な被弾にも気をつけてほしい。";
		}else if (rankN == 0) {
			comment = "稀にいる、オーラ状に光る敵を破壊すると多くのスコアを獲得できる。\n" +
				"また、狙いを正確にし、無駄な弾を撃たないことが重要である。";
		}else if (rankN == 5) {
			comment = "素晴らしい腕前だ。\n今後の更なる活躍に期待している。";
		}
	}


}