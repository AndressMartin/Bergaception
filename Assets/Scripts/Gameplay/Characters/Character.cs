using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private int vida;
    private int vidaMax;
    private int dano;
    private int velocidade;
    private CharacterSO stats;
    private Animator animator;
    public int Dano => dano;

    public void Init(CharacterSO stats) 
    {
        vidaMax = stats.vidaMax;
        vida = vidaMax;
        dano = stats.dano;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = stats.animatorOverride;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReceberDano(int dano)
    {
        vida -= dano;
        return true;
    }
}
