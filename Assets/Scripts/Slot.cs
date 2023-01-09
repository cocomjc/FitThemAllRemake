using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private GameParam gameParam;

    private void Start()
    {
        gameParam = FindObjectOfType<GameParamHandler>().gameParam;
        GetComponent<GridLayoutGroup>().cellSize = gameParam.piecesSize;
    }

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
}