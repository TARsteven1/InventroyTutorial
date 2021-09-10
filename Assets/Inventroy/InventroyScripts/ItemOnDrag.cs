using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
    public Transform originaParent;

    public Inventroy myBag;
    public int currentitemID;//当前物品id
    public void OnBeginDrag(PointerEventData eventData)
    {
        originaParent = transform.parent;

        currentitemID = originaParent.GetComponentInParent<Slot>().slotID;
        //为了提高显示层级，将所拖拽的物品设置成父物体的父物体！great！
        transform.SetParent(transform.parent.parent);
        //获得鼠标位置
        transform.position = eventData.position;
        //射线阻挡关闭
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //获得鼠标位置
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "Itemimg")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.position = originaParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originaParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            //itemlist 存储位置变换（数据变换）
            var temp = myBag.Itemlist[currentitemID];
            myBag.Itemlist[currentitemID] = myBag.Itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
            myBag.Itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.name !=null) {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform,false);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

            myBag.Itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.Itemlist[currentitemID];

            //解决自己放在自己身上被设置为空
            if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentitemID)
                myBag.Itemlist[currentitemID] = null;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        //物品拖拽失败，归位 
        transform.SetParent(originaParent);
        transform.position = originaParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;




    }


}
