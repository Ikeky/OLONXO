using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string name;
    public string category;
    public bool isStackable;
    public int countItem;
	public int value;
    [Multiline(5)]
    public string descriptionItem;
    public string pathIcon;
    public string pathPrefab;
}
