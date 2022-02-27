using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjetivoPedraContador : MonoBehaviour
{
    [SerializeField] private int pedrasAcertadas;
    [SerializeField] private int pedrasParaAcertar;

    [SerializeField] private UnityEvent eventos;

    private void Start()
    {
        pedrasAcertadas = 0;
    }

    public void SomarPedra()
    {
        pedrasAcertadas++;

        if(pedrasAcertadas >= pedrasParaAcertar)
        {
            EventoPedraCompleto();
        }
    }

    private void EventoPedraCompleto()
    {
        eventos.Invoke();
    }
}
