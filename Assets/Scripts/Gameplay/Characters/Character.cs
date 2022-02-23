using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    private int vida;
    private int vidaMax;
    private int dano;
    private int velocidade;
    private CharacterSO stats;
    private Animator animator;
    public UnityAction recebeuDano;
    public int Dano => dano;
    private void Awake()
    {
        
    }
    public void Init(CharacterSO stats) 
    {
        vidaMax = stats.vidaMax;
        vida = vidaMax;
        dano = stats.dano;
        animator = GetComponent<Animator>();
        if (animator.runtimeAnimatorController)
            animator.runtimeAnimatorController = stats.animatorOverride;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReceberDano(int dano)
    {
        vida -= dano;
        recebeuDano?.Invoke();
        return true;
    }
}
