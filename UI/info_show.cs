using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class info_show : MonoBehaviour{
	public PlayerMovement P_m;
	public PlayerShoot P_s;
	public PlayerEquip P_e;
	public PlayerHealth P_h;
	[HideInInspector]
	public GameObject GUI;
	public GameObject Message_Panel;

	bool showed = false;
	void OnTriggerEnter(Collider Player){
		if (!showed && Player.gameObject.tag == "Player") {
			ShowMessage ();
		}
	}

	public void ShowMessage(){
		Message_Panel.SetActive (true);
		GUI.SetActive (false);
		P_m.enabled = false;
		P_s.enabled = false;
		P_e.enabled = false;
		P_h.enabled = false;
		P_e.blocked = true;
		P_e.ShowInventory ();
	}

	public void CloseMessage(){
		showed = true;
		Message_Panel.SetActive (false);
		GUI.SetActive (true);
		P_m.enabled = true;
		P_s.enabled = true;
		P_e.enabled = true;
		P_h.enabled = true;
		P_e.blocked = false;
		P_e.ShowInventory ();
	}
}
