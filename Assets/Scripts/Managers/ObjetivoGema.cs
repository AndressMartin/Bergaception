using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoGema : MonoBehaviour
{
    bool ativo = false;
    public void Ativar()
    {
        ativo = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!ativo)
        {
            return;
        }
        if(other.CompareTag("Player"))
        {
            Debug.Log("terminar jogo");
        }
    }
}
