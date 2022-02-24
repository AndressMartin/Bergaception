using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtivadorDeArmadilha : MonoBehaviour
{
    private bool ativo;
    [SerializeField] private bool soSeAtivaUmaVez;
    [SerializeField] private UnityEvent eventos;

    private void Start()
    {
        ativo = true;
    }

    public void SetAtivo(bool ativo)
    {
        this.ativo = ativo;
    }

    public void AtivarArmadilha()
    {
        eventos?.Invoke();
    }

    private void OnTriggerEnter(Collider colisao)
    {
        if(ativo == false)
        {
            return;
        }

        if (colisao.CompareTag("Player"))
        {
            AtivarArmadilha();

            if(soSeAtivaUmaVez == true)
            {
                ativo = false;
            }
        }
    }
}
