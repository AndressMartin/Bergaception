using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkipContent : MonoBehaviour
{
    public UnityEvent responseSkip;
    public UnityEvent responsePlay;
    public void SkipIfRespawn()
    {
        if(PlayerPrefs.GetInt("tutorialPlayed") == 1)
        {
            responseSkip?.Invoke();
        }
        else
        {
            responsePlay?.Invoke();
        }
    }

}
