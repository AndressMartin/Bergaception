using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other?.GetComponent<Character>()?.ReceberDano(transform.root.GetComponent<Character>().Dano);
    }
}
