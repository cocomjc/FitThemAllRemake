using Array2DEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public event Action PickupEvent;
    public event Action<bool> EndPickupEvent;
    public event Action CheckCanFix;
    public event Action<bool> TriggerGlow;
    public VoidEvent onGameReset;
    public VoidEvent checkWin;
    private bool isDragged = false;
    [HideInInspector] public bool isReseting = false;
    [HideInInspector] public List<bool> canFix = new List<bool>();

    
    private void OnEnable()
    {
        onGameReset.OnEventRaised += ResetBlock;
    }

    private void OnDisable()
    {
        onGameReset.OnEventRaised -= ResetBlock;
    }
    
    public void TriggerPickUp()
    {
        if (PickupEvent != null)
        {
            GetComponent<BlockLerp>().ToggleScale(false);
            Transform parentObj = transform.parent;
            transform.SetParent(parentObj.parent);
            transform.SetParent(parentObj);
            PickupEvent();
            isDragged = true;
        }
    }

    public void EndPickUp()
    {
        if (CheckCanFix != null && EndPickupEvent != null)
        {
            canFix.Clear();
            CheckCanFix();
            isDragged = false;
            foreach (bool res in canFix)
            {
                if (!res || isReseting)
                {
                    EndPickupEvent(false);
                    GetComponent<BlockLerp>().ToggleScale(true);
                    return;
                }
            }
            EndPickupEvent(true);
            if (checkWin != null)
            {
                checkWin.RaiseEvent();
            }
        }
    }

    private void ResetBlock()
    {
        TriggerPickUp();
        Recall();
        EndPickUp();
    }

    public void Recall()
    {
        GetComponent<BlockLerp>().SetPos(GetComponent<BlockSetUp>().initPos);
        isReseting = true;
    }

    void FixedUpdate()
    {
        if (isDragged)
        {
            transform.position = Input.mousePosition - new Vector3(GetComponent<BlockSetUp>().shape.GetCells().GetLength(0) * 50, -GetComponent<BlockSetUp>().shape.GetCells().GetLength(1) * 50, 0);

            if (TriggerGlow != null)
            {
                canFix.Clear();
                CheckCanFix();
                foreach (bool res in canFix)
                {
                    if (!res)
                    {
                        TriggerGlow(false);
                        return;
                    }
                }
                TriggerGlow(true);
            }
        }
    }
}