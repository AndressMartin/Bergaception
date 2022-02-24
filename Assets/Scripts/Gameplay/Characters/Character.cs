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
    public Weapon weapon;

    [SerializeField] private ItemColetavel item;

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
    public void InteracaoItem()
    {
        if (item == null)   //pegar Item
        {
            FindObjectOfType<Interacao>().Interagir();
        }
        else if (item != null) //Usar item
        {     
            UsarItem();
        }
    }
    public void SoltarItem()
    {
        if (item != null)
        {     
            DroparItem();   
        }
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
    public void DroparItem()
    {
        item.Dropar();
        item = null;
    }

    public void PegueiItenChao(ItemColetavel _item)
    {
        item = _item;
    }
    public void UsarItem()
    {
        item.UsarItem();   
    }
}
