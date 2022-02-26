using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public List<LogEvents> logEvents = new List<LogEvents>();
    private List<VoidEvent> eventsFound = new List<VoidEvent>();
    public List<EventsAndListeners> eventsAndListeners = new List<EventsAndListeners>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetEvents());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugTest()
    {
        Debug.Log("Test");
    }

    public IEnumerator GetEvents()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Getting the list of all events...");
        var assets = AssetDatabase.FindAssets("t:VoidEvent", new[] { "Assets/Event System/Events/Void" });
        eventsAndListeners = new List<EventsAndListeners>(new EventsAndListeners[assets.Length]);

        foreach (var guid in assets)
        {
            var asset = AssetDatabase.LoadAssetAtPath<VoidEvent>(AssetDatabase.GUIDToAssetPath(guid));
            eventsFound.Add(asset);
        }
        for (int i = 0; i < assets.Length; i++)
        {
            eventsAndListeners[i] = new EventsAndListeners();
            eventsAndListeners[i].myEvent = eventsFound[i];
            foreach (var listener in eventsFound[i].GetEvents())
            {
                eventsAndListeners[i].listeners.Add(listener);
            }
        }
        Debug.Log("Events list was successfully saved.");
    }
}

[Serializable]
public class LogEvents 
{ 
    public VoidEvent voidEvent;
    public string log;
}
[Serializable]
public class EventsAndListeners 
{
    public VoidEvent myEvent;
    public List<VoidListener> listeners = new List<VoidListener>();
}


