using UnityEngine;
using System.Collections;

/// <summary>
///  ワールド座標からUI表示を出す
///  UI側に貼って,ワールド上の追いかけたいオブジェクトをtargetに紐づけ
/// T///////////////// </summary>

public class Target : MonoBehaviour {

	RectTransform rectTf;
	public Transform target;


	void Start () {
	
		rectTf = GetComponent<RectTransform> ();
	}
	

	void Update () {
	
		rectTf.position = RectTransformUtility.WorldToScreenPoint (Camera.main, target.position);
		//Debug.Log(Camera.main.ScreenToViewportPoint(rectTf.position));
		//Debug.Log(rectTf.position);

	}
}
