using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMuteButton : MonoBehaviour
{
    [SerializeField] private Sprite[] stateSprites = new Sprite[2];

    public void Start()
    {
        int muted = PlayerPrefs.GetInt("muted", 0);
        PlayerPrefs.SetInt("muted", muted == 1 ? 0 : 1);
        MuteToggle();
    }
    
    public void MuteToggle()
    {
        int muted = PlayerPrefs.GetInt("muted", 0);
        GetComponent<ScalePulse>().StartPulse();
        PlayerPrefs.SetInt("muted", muted == 1 ? 0 : 1);
        switch (muted)
        {
            case 0:
                GetComponent<Image>().sprite = stateSprites[0];
                AudioListener.volume = 1;
                break;
            case 1:
                GetComponent<Image>().sprite = stateSprites[1];
                AudioListener.volume = 0;
                break;
        }
    }

    
}
