using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquip : MonoBehaviour {
	public LayerMask[] layers;
    public Text ItemShow;
	public Transform Camera;
    public Image Point;
    public Color pointColor;
    public Color hoverColor;
    public float EquipDistance = 10f;
    public KeyCode showInventory;
    public GameObject CellGrid;
	public bool blocked = false;
    public List<Item> Inventory;

	Transform Player;
    public bool InventoryShowed;
    void Awake()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
        Inventory = new List<Item>();
        for (int i = 0; i < CellGrid.transform.childCount; i++)
        {
            Inventory.Add(new Item());
            Transform cell = CellGrid.transform.GetChild(i);
            Transform icon = cell.GetChild(1);
            Text countItem = icon.transform.GetChild(0).GetComponent<Text>();
            countItem.text = "";
        }

        ChangeInventory ();
    }
	void FixedUpdate() {
        ShowInventory();
        if (!InventoryShowed) {
            CheckItem();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pick();
            }
        }
		if(Input.GetKeyDown(KeyCode.C)){
			int stick = 0;
			foreach(Item itemer in Inventory){
				if(itemer.category == "Палка" && itemer.countItem >= 7){
					stick = 7;
					itemer.countItem-=7;
					ChangeInventory();
				} else {
					ItemShow.text = "У вас не хватает материалов";
				}
			}
			if(stick >= 7){
				CraftCampfire();
			}
		}
	}
	void CraftCampfire(){
		Player.GetChild (0).GetChild (0).gameObject.SetActive (true);
	}
	public void ShowInventory(){
        if (blocked || Input.GetKeyDown(showInventory)){
			if (CellGrid.activeSelf || blocked){
                InventoryShowed = false;
                Point.gameObject.SetActive(true);
				CellGrid.SetActive(false);
				Player.GetComponent<PlayerMovement> ().enabled = true;
				Player.GetComponent<PlayerShoot> ().enabled = true;
				Player.GetComponent<PlayerHealth> ().enabled = true;
            }else{
                InventoryShowed = true;
                Point.gameObject.SetActive(false);
                CellGrid.SetActive(true);
				Player.GetComponent<PlayerMovement> ().enabled = false;
				Player.GetComponent<PlayerShoot> ().enabled = false;
				Player.GetComponent<PlayerHealth> ().enabled = false;
            }
        }
    }
    private void CheckItem()
    {
        Ray pickRay = new Ray(transform.position, Camera.forward);
        RaycastHit pickHit;
        foreach (LayerMask layer in layers)
        {
            if (Physics.Raycast(pickRay, out pickHit, EquipDistance,layer.value))
            {
                Point.color = Color.Lerp(Point.color, hoverColor, 2f);
                ItemShow.text = pickHit.transform.GetComponent<Item>().name;
            } else{
                Point.color = Color.Lerp(Point.color, pointColor, 2f);
                ItemShow.text = "";
            }
        }
    }
	void Pick(){
		Ray pickRay = new Ray (transform.position, Camera.forward);
		RaycastHit pickHit;
		foreach(LayerMask layer in layers ){
			if (Physics.Raycast (pickRay, out pickHit, EquipDistance, layer.value)) {
                Item itemEq = pickHit.transform.GetComponent<Item>();
                if (itemEq != null){
                    AddItem(itemEq);
                }
            }
		}
	}
    void AddItem(Item currentItem)
    {
        if (currentItem.isStackable)
        {
            AddStackableItem(currentItem);
        }else{
            AddUnstackableItem(currentItem);
        }
    }
    void AddUnstackableItem(Item itemEq)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].id == 0)
            {
                Inventory[i] = itemEq;
                ChangeInventory();
                Destroy(itemEq.gameObject);
                break;
            }
        }
    }
    void AddStackableItem(Item currentItem){
        foreach(Item itemer in Inventory){
            if(itemer.id == currentItem.id){
                itemer.countItem += currentItem.countItem;
				ChangeInventory ();
                Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnstackableItem(currentItem);
    }
    public void ChangeInventory()
    {
        for(int i=0; i < Inventory.Count; i++)
        {
            Transform cell = CellGrid.transform.GetChild(i);
            Transform icon = cell.GetChild(1);
            Image iconImg = icon.GetComponent<Image>();
            Text iconText = cell.GetChild(2).gameObject.GetComponent<Text>();
            Text countItem = icon.transform.GetChild(0).GetComponent<Text>();
            if (Inventory[i].id != 0)
            {
                iconImg.enabled = true;
                iconText.enabled = true;
                iconText.text = Inventory[i].name;
                iconImg.sprite = Resources.Load<Sprite>(Inventory[i].pathIcon);
				if (Inventory [i].countItem == 1) {
					countItem.text = "";
				} else {
					countItem.text = Inventory[i].countItem.ToString();
				}

            } else {
                iconText.enabled = false;
                iconImg.enabled = false;
            }
        }
    }
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, Camera.forward * 10f);
	}
}
