using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunDirection : MonoBehaviour {

	public GameObject aim;
	public GameObject mazzle;
	public LayerMask layerMask;
	public GameObject aimPoint;

	//照準UIのテキストを紐づけ
	public Text disText;
	private Vector3 pos;


	void Update () {

		this.transform.LookAt (aim.transform);

		//敵に当たると照準の色が変わる
		Ray ray = new Ray (mazzle.transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 30.0f)) {

			//hitの情報を取得する
			int hitLayer = hit.collider.gameObject.layer;
			string layerMask;
			layerMask = LayerMask.LayerToName (hitLayer);

			if ((layerMask == "Enemy") ||(layerMask == "EnemyShot")) {
				//照準を赤に変える
				aimPoint.GetComponent<Image>().color = new Color(1f, 0.0f, 0.0f, 0.5f);

				//照準が赤い間は,対象物との距離をUI表示
				pos = new Vector3 (this.transform.position.x,this.transform.position.y,this.transform.position.z);
				Vector3 enemPos = hit.collider.gameObject.transform.position;
				float enemDis = Vector3.Distance (pos, enemPos);
				disText.text = enemDis.ToString();

			} else {
				aimPoint.GetComponent<Image>().color = new Color(0.0f, 1f, 1f, 0.5f);
				disText.text = "";
			}
					
		} else {
			aimPoint.GetComponent<Image>().color = new Color(0.0f, 1f, 1f, 0.5f);
			disText.text = "";
		}

		Debug.DrawRay(ray.origin, ray.direction*10, Color.red, 0.0f);

	}

}
