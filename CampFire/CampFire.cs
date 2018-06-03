using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampFire : MonoBehaviour {
	public Slider TimeDepender;
	bool SetBool = false;
	void FixedUpdate() {
		if (Input.GetKeyDown (KeyCode.T) && SetBool) {
			GameObject Campfire = Instantiate (gameObject,transform.position,transform.rotation);
			Campfire.GetComponent<BoxCollider>().isTrigger = false;
			Campfire.GetComponent<CampFire> ().enabled = false;
			Campfire.AddComponent<RealCampFire> ();
			Campfire.GetComponent<RealCampFire> ().TimeDepender = TimeDepender;
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider arg){
		if (arg.gameObject.layer == 9) {
			SetBool = false;
		} else {
			SetBool = true;
		}
	}
	void OnTriggerExit(Collider arg){
		if (arg.gameObject.layer == 9) {
			SetBool = true;
			Debug.Log (SetBool);
		} else {
			SetBool = false;
		}
	}
}
