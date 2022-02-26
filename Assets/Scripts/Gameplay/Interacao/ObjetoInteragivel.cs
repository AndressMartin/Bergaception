using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteragivel : MonoBehaviour
{
    private BoxCollider boxCollider;

    private bool ativo;
    [SerializeField] private UnityEvent eventos;

    public bool Ativo => ativo;
    public BoxCollider BoxCollider => boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        ativo = true;
    }

    public void SetAtivo(bool ativo)
    {
        this.ativo = ativo;
    }

    public void Interagir()
    {
        eventos?.Invoke();
        Debug.Log("Interacao com " + gameObject.name);
    }
}