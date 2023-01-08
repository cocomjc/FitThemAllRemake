using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinScreen : MonoBehaviour
{
    public StringEvent onShowUI;
    public VoidEvent onGameNextLevel;
    [SerializeField] private GameObject levelIndicator;
    [SerializeField] private GameObject topButtons;

    public void OnEnable()
    {
        onShowUI.OnEventRaised += ShowMenu;
    }

    public void OnDisable()
    {
        onShowUI.OnEventRaised -= ShowMenu;
    }

    public void Start()
    {
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void ShowMenu(string UIName)
    {
        if (UIName == "Win")
        {
            GetComponent<Image>().enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            levelIndicator.SetActive(false);
            topButtons.SetActive(false);
        }
    }

    public void HideMenu()
    {
        GetComponent<Image>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        levelIndicator.SetActive(true);
        topButtons.SetActive(true);
        if (onGameNextLevel != null)
        {
            onGameNextLevel.RaiseEvent();
        }
    }
}
