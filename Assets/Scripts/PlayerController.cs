using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public int hp;
	public Slider slider;
	private AudioSource hitSound;
	public GameObject cam;

	void Start () {
		//--------------ゲームフラグを確認してから移動スタートさせる

		slider.GetComponent<Slider>();
		hp = 3;
	
		hitSound = GetComponent<AudioSource> ();

	}
	
	void Update () {
		//HPを表示
		slider.value = hp;
	}


	void OnTriggerEnter(Collider col) {

		//enemyShotとヒットしたときの処理(音,画面揺れ,HP計算)
		hitSound.PlayOneShot(hitSound.clip);
		iTween.ShakePosition (cam, iTween.Hash ("x", 0.2f, "y", 0.2f, "time", 0.4f));

		if (hp > 0) {
			hp--;
		}


		
	}


	//プレイヤーのカメラ回転メソッド（必要に応じて）

}
