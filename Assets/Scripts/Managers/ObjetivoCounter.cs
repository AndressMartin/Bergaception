using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoCounter : MonoBehaviour
{
    [SerializeField] private IntEvent eventoContador;
    [SerializeField] private IntEvent eventoCompletoComInt;

    #region Objetivo das Macas

    private int macasPegas = 0;
    [SerializeField] private int macasQuePrecisamSerPegas;

    public void ObjetivoMacas(int macas)
    {
        macasPegas += macas;

        ObjetivoMacasCompleto(macasPegas);

        if (macasPegas >= macasQuePrecisamSerPegas)
        {
            macasPegas = 0;
        }
    }

    private void ObjetivoMacasCompleto(int macas)
    {
        eventoCompletoComInt.Raise(macas);
    }

    #endregion
}