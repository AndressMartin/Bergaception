using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracaoPes : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Water");
            GetComponentInParent<StarterAssets.ThirdPersonController>().LaunchUpwards();
        }
    }
}
