using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    RectTransform currentRect;

    void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //获得锚点的坐标+=鼠标上一针坐标，实现窗口拖拽
        currentRect.anchoredPosition += eventData.delta;
    }

}
