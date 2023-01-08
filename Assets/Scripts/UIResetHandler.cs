using UnityEngine;

public class UIResetHandler : MonoBehaviour
{
    public VoidEvent OnGameReset;

    public void ResetGame()
    {
        if (OnGameReset != null)
        {
            OnGameReset.RaiseEvent();
        }
    }
}
