using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracaoPes : MonoBehaviour
{
    public VoidEvent cruzouPortal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Water");
            GetComponentInParent<StarterAssets.ThirdPersonController>().LaunchUpwards();
        }
        if (other.CompareTag("Portal"))
        {
            Debug.Log("Portal");
            cruzouPortal.Raise();
        }
    }
}
