using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialog/Dialog Sequence")]
public class DialogSO : ScriptableObject
{
    public UnityEvent dialogRaised;
    public List<DialogLine> _textList;

    public void WarnEventWasSuccessful()
    {
        foreach (var DialogLine in _textList)
        {
            DialogLine.eventToWaitFor?.RegisterOutsideEvent(dialogRaised);
        }
    }
}

[Serializable]
public class DialogLine
{
    public string text;
    public float timeToWait;
    public TextType type;
    public VoidEvent eventToWaitFor;
    public VoidEvent fireAtEnd;

}

public enum TextType
{
    Continuous,
    New
}
