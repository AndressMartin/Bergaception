using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFase2 : MonoBehaviour
{
    public VoidEvent cruzouPortal;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Portal");
            cruzouPortal.Raise();
        }
    }
}
