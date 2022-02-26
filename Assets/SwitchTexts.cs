using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchTexts : MonoBehaviour
{
    public List<TextToReplace> texts = new List<TextToReplace>();

    
    public void SwitchText()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].textHolder.text = texts[i].textToReplace;
        }
    }
}
[System.Serializable]
public class TextToReplace 
{
    public TextMeshProUGUI textHolder;
    [HideInInspector]public string text;
    public string textToReplace;
}

