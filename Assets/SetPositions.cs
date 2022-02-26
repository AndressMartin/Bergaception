using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositions : MonoBehaviour
{
    public Vector3 positionToSet;
    
    public void SetPosition()
    {
        transform.position = positionToSet;
    }
}
