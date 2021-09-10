using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public int slotID;//空id =物品id
    public Item soltItem;
    public Image soltImage;
    public Text soltNum;
    public string slotInfo;

    public GameObject itemInslot;

    public void ItemOnClicked()
    {
        InventroyManage.UpdateItemInfo(slotInfo);
    }
    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInslot.SetActive(false);
            return;
        }
        soltImage.sprite = item.itemImg;
        soltNum.text = item.Itemheld.ToString();
        slotInfo = item.itemInfo;
    }
}
