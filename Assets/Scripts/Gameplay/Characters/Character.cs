using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int vida;
    [SerializeField]
    private int vidaMax;
    private int dano;
    private int velocidade;
    private CharacterSO stats;
    private Animator animator;
    public UnityAction recebeuDano;
    public Weapon weapon;
    public int Dano => dano;
    public int Vida => vida;
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

    public virtual bool ReceberDano(int dano)
    {
        vida -= dano;
        recebeuDano?.Invoke();
        return true;
    }

    public virtual void ReceberKnockBack(Vector3 directionOfKnockback)
    {
        Quaternion rotation = Quaternion.LookRotation(directionOfKnockback, Vector3.up);
        transform.rotation = Quaternion.Inverse(rotation);
    }

    public void ToggleWeaponCollide()
    {
        weapon.ToggleAttack();
    }
}
