using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public VoidEvent onGameFinish;
    public VoidEvent checkWin;
    [SerializeField] private GameParam gameParam;

    public void OnEnable()
    {
        Debug.Log("GridManager OnEnable set size to" + GetComponent<GridLayoutGroup>().cellSize);
        gameParam.piecesSize = GetComponent<GridLayoutGroup>().cellSize;
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
