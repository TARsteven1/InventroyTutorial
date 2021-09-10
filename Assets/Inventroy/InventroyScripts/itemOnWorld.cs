using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour {

	public Item thisitem;
	public Inventroy mybag;
    private void OnEnable()
    {
       // mybag.Itemlist.Clear();
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
            AddNeewItem();
            Destroy(gameObject);
        }
    }

    private void AddNeewItem()
    {
        if (!mybag.Itemlist.Contains(thisitem))
        {
            // mybag.Itemlist.Add(thisitem);
            //InventroyManage.CreateNewItem(thisitem);
            for (int i = 0; i < mybag.Itemlist.Count; i++)
            {
                if (mybag.Itemlist[i]==null)
                {
                    mybag.Itemlist[i] = thisitem;
                    break;
                }
            }
        }
        else
        {
            thisitem.Itemheld += 1;
           
        }
        InventroyManage.RefreshItem();
    }
  

}
