using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public static GameObject itemDragging;

    Vector3 Posicionini;
    Transform iniparent;
    Transform dragParent;

    void Start()
    {
        dragParent = GameObject.FindGameObjectWithTag("dragparent").transform;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragging = gameObject;

        Posicionini = transform.position;
        iniparent = transform.parent;
        transform.SetParent(dragParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragging = null;
        if (transform.parent == dragParent)
        {
            transform.position = Posicionini;
            transform.SetParent(iniparent);
        }
    }

    void Update()
    {
        
    }
}
