using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maca : ItemColetavel
{
    protected override int Id => 1;

    [SerializeField] private IntEvent eventoContador;

    public void SeAtivar()
    {
        GetComponent<ObjetoInteragivel>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void UsarItem()
    {
        base.UsarItem();
    }

    public override void PegarItem()
    {
        character.PegueiItenChao(this);
        gameObject.SetActive(false);
        character.Hud.TrocarIconeItem(Id);

        eventoContador?.Raise(1);
    }

    public override void Dropar()
    {
        UsarItem();
    }
}
