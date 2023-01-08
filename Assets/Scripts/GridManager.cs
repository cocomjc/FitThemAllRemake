using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public VoidEvent onGameFinish;
    public VoidEvent checkWin;

    public void OnEnable()
    {
        checkWin.OnEventRaised += CheckWin;
    }

    public void OnDisable()
    {
        checkWin.OnEventRaised -= CheckWin;
    }
    
    private void CheckWin()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                return;
            }
        }
        if (onGameFinish != null)
        {
            onGameFinish.RaiseEvent();
        }
    }
}
