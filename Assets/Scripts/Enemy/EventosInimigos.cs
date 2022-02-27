using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosInimigos : MonoBehaviour
{
    [SerializeField] IntEvent intevent;

    public void ChamarEvento()
    {
        intevent.Raise(1);
    }
}
