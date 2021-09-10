using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventroyManage : MonoBehaviour {

	// Use this for initialization
	static InventroyManage Instance;
	public Inventroy myBag;
	public GameObject soltGrid;
	//public Slot soltPrefabs;
	public GameObject emptySlot;
	public Text itemInformation;

	public List<GameObject> slots = new List<GameObject>();
	void Awake () {
        if (Instance!=null)
        {
			Destroy(this);
        }
		Instance = this;
	}
	private void OnEnable()
    {
		RefreshItem();
		Instance.itemInformation.text = "";
    }
	//public static void CreateNewItem(Item item)
 //   {
	//	Slot newitem = Instantiate(Instance.soltPrefabs, Instance.soltGrid.transform.position,Quaternion.identity);
	//	newitem.gameObject.transform.SetParent(Instance.soltGrid.transform);
	//	newitem.soltItem = item;
	//	newitem.soltImage.sprite = item.itemImg;
	//	newitem.soltNum.text = item.Itemheld.ToString();
 //   }

	public static void RefreshItem()
    {
        for (int i = 0; i < Instance.soltGrid.transform.childCount; i++)
        {
            if (Instance.soltGrid.transform.childCount==0)
            {
				break;
            }
			Destroy(Instance.soltGrid.transform.GetChild(i).gameObject);
			Instance.slots.Clear();
		}
        for (int i = 0; i < Instance.myBag.Itemlist.Count; i++)
        {
			//CreateNewItem(Instance.myBag.Itemlist[i]);
			Instance.slots.Add(Instantiate(Instance.emptySlot));
			Instance.slots[i].transform.SetParent(Instance.soltGrid.transform);
			Instance.slots[i].GetComponent<Slot>().slotID = i;
			Instance.slots[i].GetComponent<Slot>().SetupSlot(Instance.myBag.Itemlist[i]);

		}
		
	}
	public static void UpdateItemInfo(string itemInfo)
    {
		Instance.itemInformation.text = itemInfo;
    }
	// Update is called once per frame

}
