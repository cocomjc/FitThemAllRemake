using UnityEngine;
using TMPro;

public class UILevelUpdater : MonoBehaviour
{
    public VoidEvent onGameStart;

    private void OnEnable()
    {
        onGameStart.OnEventRaised += UpdateUi;
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("LevelName");
    }

    private void OnDisable()
    {
        onGameStart.OnEventRaised -= UpdateUi;
    }
    private void UpdateUi()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("LevelName");
    }
}
