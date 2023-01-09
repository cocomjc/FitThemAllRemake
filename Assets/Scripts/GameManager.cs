using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public VoidEvent onGameStart;
    public VoidEvent onGameFinish;
    public VoidEvent onGameNextLevel;
    public StringEvent onShowUI;
    [SerializeField] private bool resetPlayerPrefs = false;
    [SerializeField] private int startAtLevel = 0;

    public void OnEnable()
    {
        onGameFinish.OnEventRaised += LevelEnd;
        onGameNextLevel.OnEventRaised += LoadNextLevel;
    }

    public void OnDisable()
    {
        onGameFinish.OnEventRaised -= LevelEnd;
        onGameNextLevel.OnEventRaised -= LoadNextLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") == 0 || resetPlayerPrefs)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (startAtLevel > 0)
        {
            PlayerPrefs.SetInt("Level", startAtLevel);
        }
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"), LoadSceneMode.Additive);
        PlayerPrefs.SetString("LevelName", SceneManager.GetSceneByBuildIndex(PlayerPrefs.GetInt("Level")).name);
        if (onGameStart != null)
        {
            onGameStart.RaiseEvent();
        }
    }

    private void LevelEnd()
    {
        if (onShowUI != null)
        {
            onShowUI.RaiseEvent("Win");
        }
    }
    
    public void LoadNextLevel()
    {
        SceneManager.UnloadSceneAsync(PlayerPrefs.GetInt("Level"));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        LoadLevel();
        if (onShowUI != null)
        {
            onShowUI.RaiseEvent("Home");
        }
    }    
}
