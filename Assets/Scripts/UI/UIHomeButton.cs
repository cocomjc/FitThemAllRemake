using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHomeButton : MonoBehaviour
{
    public StringEvent onShowUI;
    
    public void ShowMenu() {
        if (onShowUI != null)
        {
            onShowUI.RaiseEvent("Win");
        }
    }
}
