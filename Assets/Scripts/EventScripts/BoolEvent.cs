using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Event System/Bool Event")]
public class BoolEvent : ScriptableObject
{
    private readonly List<BoolListener> eventListeners =
        new List<BoolListener>();

    public void Raise(bool a)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(a);
        }
    }

    public void RegisterListener(BoolListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(BoolListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
