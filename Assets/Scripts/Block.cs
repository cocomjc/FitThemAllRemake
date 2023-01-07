using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using System;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public event Action PickupEvent;
    public event Action <bool> EndPickupEvent;
    public event Action CheckCanFix;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Array2DBool shape;
    private bool isDragged = false;
    public List<bool> canFix = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject newPiece = null;

        for (int i = 0; i < shape.GetCells().GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetCells().GetLength(1); j++)
            {
                if (shape.GetCells()[i,j])
                {
                    //Debug.Log("i: " + i + " j: " + j);
                    newPiece = Instantiate(piecePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    newPiece.transform.SetParent(transform);
                    newPiece.GetComponent<DraggableItem>().SetUpDraggable(gameObject);
                    newPiece.transform.localPosition = new Vector3(j*100, -i*100, 0);
                    newPiece.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void TriggerPickUp()
    {
        if (PickupEvent != null)
        {
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
                if (!res)
                {
                    EndPickupEvent(false);
                    return;
                }
            }
            EndPickupEvent(true);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("isDragged: " + isDragged);
        if (isDragged)
        {
            transform.position = Input.mousePosition;
        }
    }
}
