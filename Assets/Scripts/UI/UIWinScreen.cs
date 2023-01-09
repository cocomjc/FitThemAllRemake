using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinScreen : MonoBehaviour
{
    public StringEvent onShowUI;
    public VoidEvent onGameNextLevel;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private float fadeSpeed = .3f;
    [SerializeField] private GameObject emoji;
    [SerializeField] private Sprite[] emojiSprites;

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
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        emoji.SetActive(false);
    }

    private void ShowMenu(string UIName)
    {
        if (UIName == "Win")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(FadeCoroutine(true, fadeSpeed));
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            emoji.SetActive(true);
            emoji.GetComponent<Image>().sprite = emojiSprites[Random.Range(0, emojiSprites.Length)];
        }
    }

    public void HideMenu()
    {
        StartCoroutine(FadeCoroutine(false, fadeSpeed));
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        emoji.SetActive(false);
        if (onGameNextLevel != null)
        {
            onGameNextLevel.RaiseEvent();
        }
    }

    private IEnumerator FadeCoroutine(bool fadeIn, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            GetComponent<CanvasGroup>().alpha = fadeIn ? Mathf.Lerp(0f, 1f, elapsedTime / duration) : 1 - Mathf.Lerp(0f, 1f, elapsedTime / duration);
            if (fadeIn)
                mainUI.GetComponent<CanvasGroup>().alpha = 1 - Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GetComponent<CanvasGroup>().alpha = fadeIn ? 1f : 0f;
        mainUI.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
