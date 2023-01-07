using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
}