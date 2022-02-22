using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{

    bool playerZonaAtaque;
    public bool GetPlayerZonaAtaque => playerZonaAtaque;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerZonaAtaque = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerZonaAtaque = false;
        }
    }

}
