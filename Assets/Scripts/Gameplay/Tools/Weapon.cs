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
    public void ToggleAttackOff()
    {
        canAttack = false;
        alreadyHit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("chamando");
        if(canAttack && !alreadyHit)
        {
            if (other.transform != transform.root)
            {
                if (other.TryGetComponent(out Character character))
                {
                    if (!character.morto)
                    {
                        if (other.GetComponent<Enemy>() != null)
                        {
                            character.ReceberKnockBack(transform.root.GetComponent<ThirdPersonController>().GetDirection()); //player causando dano
                            character.ReceberDano(transform.root.GetComponent<Character>().Dano);
                        }
                        else
                        {
                            character.ReceberDano(transform.root.GetComponent<Enemy>().Dano); //inimigo causando dano
                        }
                        alreadyHit = true;
                    }
                }
            }
        }
        
    }
}
