using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteragivel : MonoBehaviour
{
    private bool ativo;
    [SerializeField]
    private UnityEvent eventos;

    public bool Ativo => ativo;

    private void Start()
    {
        ativo = true;
    }

    public void SetAtivo(bool ativo)
    {
        this.ativo = ativo;
    }

    public void Interagir()
    {
        eventos?.Invoke();
        Debug.Log("Intecao com " + gameObject.name);
    }
}