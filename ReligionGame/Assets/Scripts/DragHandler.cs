using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    private Actions _action;

    public void SetAction(Actions action)
    {
        _action = action;
    }

    public Actions GetAction()
    {
        return _action;
    }

    #region IBeginDragHandler implementation

    public void OnBeginDrag (PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;

    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag (PointerEventData eventData)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 1f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag (PointerEventData eventData)
    {
        itemBeingDragged = null;
        bool moved = false;


        Transform actionQueueTran = GameObject.Find("/Canvas/ActionQueue").transform;
        int childCount = actionQueueTran.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (transform != actionQueueTran.GetChild(i))
            {
                if (transform.position.y >= actionQueueTran.GetChild(i).position.y)
                {
                    transform.SetSiblingIndex(i);
                    moved = true;
                    break;
                }
            }
        }
        if (transform.position.y < actionQueueTran.GetChild(childCount - 1).position.y)
        {
            transform.SetAsLastSibling();
            moved = true;
        }
        if (!moved)
        {
            transform.position = startPosition;
        }

    }

    #endregion


}
