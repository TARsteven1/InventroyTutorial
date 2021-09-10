using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "Inventroy/New Item")]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite itemImg;
    public int Itemheld;
    [TextArea]
    public string itemInfo;
    public bool equip;

}
