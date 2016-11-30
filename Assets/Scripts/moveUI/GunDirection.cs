using UnityEngine;
using System.Collections;

public class GunDirection : MonoBehaviour {

	public GameObject aim;

	void Update () {

		this.transform.LookAt (aim.transform);

	}
}
