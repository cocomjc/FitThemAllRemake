using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public Image mainImage;
    [SerializeField] private GameObject glow;
    [SerializeField] private GameParam gameParam;
    [HideInInspector] private GameObject blockParent = null;
    private GameObject freeSlot = null;
    private Vector2 initPos;
    private BlockManager blockManager;

    
    public void SetUpDraggable(GameObject _blockParent, Vector2 _initPos)
    {
        blockParent = _blockParent;
        blockManager = blockParent.GetComponent<BlockManager>();
        initPos = _initPos;

        GetComponent<RectTransform>().sizeDelta = gameParam.piecesSize;

        //Sub to events
        blockManager.PickupEvent += GroupToParentBlock;
        blockManager.CheckCanFix += CheckIfAnySlot;
        blockManager.EndPickupEvent += DropPiece;
        blockManager.TriggerGlow += Glow;
    }

    public void OnDisable()
    {
        blockManager.PickupEvent -= GroupToParentBlock;
        blockManager.CheckCanFix -= CheckIfAnySlot;
        blockManager.EndPickupEvent -= DropPiece;
        blockManager.TriggerGlow -= Glow;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        blockManager.TriggerPickUp();
    }

    public void OnDrag(PointerEventData eventData) {}

    public void OnEndDrag(PointerEventData eventData)
    {
        blockManager.EndPickUp();
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
            blockManager.Recall();
            transform.localPosition = initPos;
        }
        glow.transform.position = transform.position;
        glow.GetComponent<Image>().enabled = false;
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
                    blockManager.canFix.Add(true);
                    return;
                }
            }
        };
        blockManager.canFix.Add(false);
        return;
    }

    public void Glow(bool isGlowing)
    {
        glow.transform.position = transform.position;
        glow.GetComponent<Image>().enabled = true;
        if (isGlowing)
        {
            glow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.44f, .8f);
        }
        else
        {
            glow.GetComponent<Image>().color = new Color(0f, 0f, 0f, .6f);
        }
    }
}