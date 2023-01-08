using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public Image mainImage;
    [SerializeField] private Image glow;
    [HideInInspector] private GameObject blockParent = null;
    private GameObject freeSlot = null;
    private Vector2 initPos;


    public void SetUpDraggable(GameObject _blockParent, Vector2 _initPos)
    {
        blockParent = _blockParent;
        initPos = _initPos;
        blockParent.GetComponent<BlockSetUp>().PickupEvent += GroupToParentBlock;
        blockParent.GetComponent<BlockSetUp>().CheckCanFix += CheckIfAnySlot;
        blockParent.GetComponent<BlockSetUp>().EndPickupEvent += DropPiece;
        blockParent.GetComponent<BlockSetUp>().TriggerGlow += Glow;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        blockParent.GetComponent<BlockSetUp>().TriggerPickUp();
    }

    public void OnDrag(PointerEventData eventData) {}

    public void OnEndDrag(PointerEventData eventData)
    {
        blockParent.GetComponent<BlockSetUp>().EndPickUp();
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
        else {
            blockParent.GetComponent<BlockSetUp>().Recall();
            transform.localPosition = initPos;
        }
        glow.enabled = false;
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
                    blockParent.GetComponent<BlockSetUp>().canFix.Add(true);
                    return;
                }
            }
        };
        blockParent.GetComponent<BlockSetUp>().canFix.Add(false);
        return;
    }

    public void Glow(bool isGlowing)
    {
        glow.enabled = true;
        if (isGlowing)
        {
            glow.color = new Color(0.5f, 1f, 0.44f, .8f); ;
        }
        else
        {
            glow.color = new Color(0f, 0f, 0f, .8f); ;
        }
    }
}