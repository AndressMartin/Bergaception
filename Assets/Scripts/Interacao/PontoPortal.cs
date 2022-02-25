using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoPortal : MonoBehaviour
{
    private enum Ponto { Entrada, Saida };
    [SerializeField] private Ponto ponto;

    Teleport teleport;

    bool colidindo;
    public bool GetColidindo => colidindo;


    private void Start()
    {
        teleport = GetComponentInParent<Teleport>();
        switch (ponto)
        {
            case Ponto.Entrada:
                teleport.ReceberEntrada(this);
                break;
            case Ponto.Saida:
                teleport.ReceberSaida(this);

                break;
        }
    }
    public void Interacao()
    {
        if (teleport != null)
        {
            teleport.Interagir();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            colidindo = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            colidindo = false;
        }
    }
}
