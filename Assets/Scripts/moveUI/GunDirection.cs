using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunDirection : MonoBehaviour {

	public GameObject aim;
	public GameObject mazzle;
	public LayerMask layerMask;
	public GameObject aimPoint;
	//public GameObject target;

	void Update () {

		this.transform.LookAt (aim.transform);

		//敵に当たると照準の色が変わる
		Ray ray = new Ray (mazzle.transform.position, transform.forward);
		RaycastHit hit;
		//if (NavMesh.Raycast (mazzle.transform.position, target.transform.position,  out hit, NavMesh.AllAreas)) {
		if (Physics.SphereCast (ray, 0.8f, out hit, 15.0f, layerMask)) {
			aimPoint.GetComponent<Image> ().color = new Color (1f, 0.0f, 0.0f, 0.5f);
		} else {
			aimPoint.GetComponent<Image> ().color = new Color (0.0f, 1f, 1f, 0.5f);
		}
	}
}
