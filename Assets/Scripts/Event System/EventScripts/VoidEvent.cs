using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System.Linq;

[CreateAssetMenu(menuName = "Event System/Void Event")]
public class VoidEvent : ScriptableObject
{
    private readonly List<VoidListener> eventListeners =
        new List<VoidListener>();

    private readonly List<UnityEvent> outsideEvents = new List<UnityEvent>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
        for (int i = outsideEvents.Count -1; i >= 0; i--)
        {
            outsideEvents[i].Invoke();
        }
    }

    public List<VoidListener> GetEvents()
    {
        return eventListeners;
    }

    public void RegisterOutsideEvent(UnityEvent evnt)
    {
        outsideEvents.Add(evnt);
    }

    public void RegisterListener(VoidListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(VoidListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
        if (outsideEvents.Any())
            outsideEvents.Clear();
    }
}

