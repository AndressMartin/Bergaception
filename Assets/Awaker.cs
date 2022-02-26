using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awaker : MonoBehaviour
{
    public VoidEvent awakeEvent;
    private void Awake()
    {
        awakeEvent.Raise();
    }
}
