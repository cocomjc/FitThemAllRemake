using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Array2DBool shape;
    [SerializeField] private Color blockColor;
    public event Action PickupEvent;
    public event Action <bool> EndPickupEvent;
    public event Action CheckCanFix;
    public event Action<bool> TriggerGlow;
    private bool isDragged = false;
    [HideInInspector] public List<bool> canFix = new List<bool>();
    private Vector2 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        GetComponent<Image>().enabled = false;
        Debug.Log("offset: " + shape.GetCells().GetLength(0) * 100 + " , " + shape.GetCells().GetLength(1) * 100);
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
                    newPiece.GetComponent<DraggableItem>().SetUpDraggable(gameObject, new Vector3(j * 100, -i * 100, 0));
                    newPiece.GetComponent<DraggableItem>().mainImage.color = blockColor;
                    newPiece.transform.localPosition = new Vector3(j*100, -i*100, 0);
                    newPiece.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        transform.localScale = new Vector3(.4f, .4f, 1f);
    }

    public void TriggerPickUp()
    {
        if (PickupEvent != null)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
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
                    transform.localScale = new Vector3(.4f, .4f, 1f);
                    return;
                }
            }
            EndPickupEvent(true);

        }
    }

    public void Recall()
    {
        transform.position = initPos;
    }    

    void FixedUpdate()
    {
        //Debug.Log("isDragged: " + isDragged);
        if (isDragged)
        {
            transform.position = Input.mousePosition - new Vector3(shape.GetCells().GetLength(0) * 50, -shape.GetCells().GetLength(1) * 50, 0);

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
