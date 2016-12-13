using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public int hp;
	public Slider slider;
	private AudioSource audio;

	void Start () {
		//--------------ゲームフラグを確認してから移動スタートさせる

		slider.GetComponent<Slider>();
		hp = 3;
	
		audio = GetComponent<AudioSource> ();

	}
	
	void Update () {
		//HPを表示
		slider.value = hp;
	}


	void OnTriggerEnter(Collider col) {

		//enemyShotとヒットしたときの処理（hp--,hp残り判定,音、エフェクト）
		audio.PlayOneShot(audio.clip);

		if (hp > 0) {
			hp--;
		}


		
	}


	//プレイヤーのカメラ回転メソッド（必要に応じて）

}
