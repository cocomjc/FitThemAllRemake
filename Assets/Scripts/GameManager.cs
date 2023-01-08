using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public VoidEvent onGameStart;
    public VoidEvent onGameFinish;
    [SerializeField] private bool resetPlayerPrefs = false;

    public void OnEnable()
    {
        onGameFinish.OnEventRaised += LoadNextLevel;
    }

    public void OnDisable()
    {
        onGameFinish.OnEventRaised -= LoadNextLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") == 0 || resetPlayerPrefs)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"), LoadSceneMode.Additive);
        if (onGameStart != null)
        {
            onGameStart.RaiseEvent();
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.UnloadSceneAsync("Level" + PlayerPrefs.GetInt("Level"));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        LoadLevel();
    }    
}
