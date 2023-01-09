using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private GameParam gameParam;

    private void Start()
    {
        GetComponent<GridLayoutGroup>().cellSize = gameParam.piecesSize;
    }

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
}