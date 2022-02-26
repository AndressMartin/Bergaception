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
        if (cinematicOn) startCinematic.Raise();
        else skipCinematic.Raise();
    }
}
