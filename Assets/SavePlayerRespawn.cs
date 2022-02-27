using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerRespawn : MonoBehaviour
{
    
    public void SetTutorialAsPlayed()
    {
        PlayerPrefs.SetInt("tutorialPlayed", 1);
    }

    public void ResetTutorialPlayed()
    {
        PlayerPrefs.SetInt("tutorialPlayed", 0);
    }
}
