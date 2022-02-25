using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VoidListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public VoidEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Debug.Log($"{Event.name } was raised by {Response.GetPersistentMethodName(0)}");
        Response.Invoke();
    }
}
