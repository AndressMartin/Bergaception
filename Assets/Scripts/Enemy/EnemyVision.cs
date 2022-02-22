using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    bool vendoPlayer;
    public bool GetPlayerVision => vendoPlayer;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vendoPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vendoPlayer = false;
        }
    }
}
