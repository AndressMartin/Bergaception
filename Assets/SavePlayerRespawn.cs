using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerRespawn : MonoBehaviour
{
    public bool tutorialPlayed;
    
    public void SetTutorialAsPlayed()
    {
        PlayerPrefs.SetInt("tutorialPlayed", 1);
    }
}
