using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoPortal : MonoBehaviour
{
    public VoidEvent cruzouPortal;

    bool ativo = false;
    public void Ativar()
    {
        ativo = true;
        Debug.Log("ativei");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("to no triger");
        if (!ativo)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            cruzouPortal.Raise();
        }
    }
    public void Chamar()
    {
        cruzouPortal.Raise();
    }
}
