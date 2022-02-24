using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool canAttack;
    public bool alreadyHit;

    public void ToggleAttack()
    {
        canAttack = !canAttack;
        alreadyHit = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(canAttack && !alreadyHit)
        {
            if (other.transform != transform.root)
            {
                if (other.TryGetComponent(out Character character))
                {
                    character.ReceberKnockBack(transform.root.GetComponent<ThirdPersonController>().GetDirection());
                    character.ReceberDano(transform.root.GetComponent<Character>().Dano);
                    alreadyHit = true;
                }
                Debug.Log(other.name);
            }
        }
        
    }
}
