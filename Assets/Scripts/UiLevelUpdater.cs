using UnityEngine;
using TMPro;

public class UiLevelUpdater : MonoBehaviour
{
    public VoidEvent onGameStart;

    private void OnEnable()
    {
        onGameStart.OnEventRaised += UpdateUi;
    }

    private void OnDisable()
    {
        onGameStart.OnEventRaised -= UpdateUi;
    }
    private void UpdateUi()
    {
        GetComponent<TextMeshProUGUI>().text = "Level " + PlayerPrefs.GetInt("Level");
    }
}
