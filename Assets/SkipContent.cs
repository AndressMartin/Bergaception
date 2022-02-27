using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkipContent : MonoBehaviour
{
    public List<UnityEvent> responses = new List<UnityEvent>();

    public void SkipIfRespawn()
    {
        if(PlayerPrefs.GetInt("tutorialPlayed") == 1)
        {
            foreach (var response in responses)
            {
                response.Invoke();
            }
        }
    }

}
