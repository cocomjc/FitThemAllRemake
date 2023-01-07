using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] private GameObject blockParent = null;
    private bool isDragged = false;
    private GameObject freeSlot = null;


    public void SetUpDraggable(GameObject _blockParent)
    {
        blockParent = _blockParent;
        blockParent.GetComponent<Block>().PickupEvent += GroupToParentBlock;
        blockParent.GetComponent<Block>().CheckCanFix += CheckIfAnySlot;
        blockParent.GetComponent<Block>().EndPickupEvent += DropPiece;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        blockParent.GetComponent<Block>().TriggerPickUp();
        isDragged = true;
    }

    public void OnDrag(PointerEventData eventData) {}

    public void OnEndDrag(PointerEventData eventData)
    {
        blockParent.GetComponent<Block>().EndPickUp();
        isDragged = true;
    }

    public void FixedUpdate()
    {
        if (isDragged)
        {
            EventSystem m_EventSystem = GetComponent<EventSystem>();
            PointerEventData eventData = new PointerEventData(m_EventSystem);
            eventData.position = transform.position;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var result in raycastResults)
                {
                    if (result.gameObject.GetComponent<Slot>() != null && result.gameObject.GetComponent<Slot>().IsOccupied() == false)
                    {
                        Debug.Log("YESS");
                        return;
                    }
                }
            };
            Debug.Log("NOOO");
        }
    }

    private void GroupToParentBlock()
    {
        //Debug.Log("GroupToParentBlock");
        transform.SetParent(blockParent.transform);
    }

    private void DropPiece(bool fixPiece)
    {
        //Debug.Log("DropPiece");
        if (fixPiece && freeSlot)
        {
            transform.SetParent(freeSlot.transform);
        }
        isDragged = false;
    }

    public void CheckIfAnySlot()
    {
        EventSystem m_EventSystem = GetComponent<EventSystem>();
        PointerEventData eventData = new PointerEventData(m_EventSystem);
        eventData.position = transform.position;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var result in raycastResults)
            {
                if (result.gameObject.GetComponent<Slot>() != null && result.gameObject.GetComponent<Slot>().IsOccupied() == false)
                {
                    freeSlot = result.gameObject;
                    blockParent.GetComponent<Block>().canFix.Add(true);
                    return;
                }
            }
        };
        blockParent.GetComponent<Block>().canFix.Add(false);
        return;
    }
}