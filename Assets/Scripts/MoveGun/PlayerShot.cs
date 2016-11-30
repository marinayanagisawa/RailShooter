using UnityEngine;
using System.Collections;

/// <summary>
/// まっすぐ飛ぶ弾
/// 銃口の直接操作(GunMove)の場合はこちらの弾で
/// 
/// 今のところ,とりあえず飛んで何かに当たると消えるだけの機能
/// P/////////// </summary>

public class PlayerShot : MonoBehaviour {

	private float shotSpeed = 0.5f;
	private float lifeTime = 2.0f;

	void Update () {
	
		this.transform.Translate (0, 0, shotSpeed);
		Destroy (this.gameObject, lifeTime);
	}

	void OnTriggerEnter(Collider col){
		
		Destroy (this.gameObject);

	}

}
