using UnityEngine;
using System.Collections;

/// <summary>
/// 弾が照準オブジェクトに向く
/// 照準操作(UImove)の場合はこちらの弾で
/// 
/// 今のところ,とりあえず飛んで何かに当たると消えるだけの機能
/// P//////////////// </summary>

public class PlayerShotFollow : MonoBehaviour {

	//private float shotSpeed = 0.4f;
	private float shotPower = 30;
	private float lifeTime = 2.0f;

	//銃のAimオブジェクトを紐づけ
	private GameObject aim;

	void Start () {
		aim = GameObject.Find ("aim");
		this.transform.LookAt (aim.transform);
		Destroy(this.gameObject, lifeTime);
		this.GetComponent<Rigidbody>().velocity = transform.forward * shotPower;
		LocalValues.shotNum++;
	}

	void Update () {

		//this.transform.Translate (0, 0, shotSpeed);
	
	}

	void OnTriggerEnter(Collider col){
		Destroy (this.gameObject);
	}

}
