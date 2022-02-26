using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableController : MonoBehaviour
{
    public bool cinematicOn;
    public VoidEvent skipCinematic;
    public VoidEvent startCinematic;
    public void StartCinematicOrNot()
    {
#if UNITY_EDITOR
        if (cinematicOn) startCinematic.Raise();
        else skipCinematic.Raise();
#endif
#if !UNITY_EDITOR
        startCinematic.Raise();
#endif
    }
}
