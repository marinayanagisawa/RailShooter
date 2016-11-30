using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject effect;

	void OnTriggerEnter(Collider col){

		transform.FindChild("Explosion").GetComponent<ParticleSystem> ().Play();
		DestroyEff (effect);
		EleaseAndDestroy ();

	}
		
	void DestroyEff(GameObject eff){
		Destroy (eff, 0.7f);
	}


	void EleaseAndDestroy(){
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;
		Destroy (this.gameObject, 1.0f);
	}


}
