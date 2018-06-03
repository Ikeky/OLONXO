using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealCampFire : MonoBehaviour {
	public float TimeToCamp = 5f; 
	public bool Camped = false;
	public Slider TimeDepender;
	Transform Player;
	PlayerEquip Equipkit;

	float timer = 0f;
	bool runtimer = false;
	int countStick = 0;
	int stickid = 0;
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		Equipkit = Player.GetComponent<PlayerEquip> ();
	}
	void Update () {
		if(Camped && Vector3.Distance(Player.position,gameObject.transform.position) <= 10){
			
		}
		if (runtimer  && TimeDepender != null)Camping ();
		if (!Camped) {
			Fire ();
		}
	}
	void Fire(){
		Ray raycast = new Ray (Player.position,Player.transform.GetChild(0).forward);
		RaycastHit hit;
		if (Physics.Raycast (raycast, out hit, Equipkit.EquipDistance,LayerMask.GetMask("campfire"))) {
			if(Input.GetKeyDown(KeyCode.E)){
				List<Item> Inventory = Equipkit.Inventory;
				for(int i = 0;i < Inventory.Count;i++){
					if (Inventory[i].id == 3 && Inventory[i].countItem >= 2) {
						countStick += 2;
						stickid = i;
						break;
					}
				}
				if (Input.GetKeyDown (KeyCode.E) && TimeDepender != null) {
					runtimer = true;
					Player.GetComponent<PlayerMovement> ().enabled = false;
					TimeDepender.gameObject.SetActive (true);
					return;
				}
			}
		}
	}
	void Camping(){ 
		timer += Time.deltaTime;
		TimeDepender.value = timer;
		if (timer >= TimeToCamp) {
			List<Item> Inventory = Equipkit.Inventory;
			Inventory [stickid].countItem -= countStick;
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (true);
			Player.GetComponent<PlayerMovement> ().enabled = true;
			runtimer = false;
			timer = 0f;
			TimeDepender.gameObject.SetActive (false);
		}
	}
}
