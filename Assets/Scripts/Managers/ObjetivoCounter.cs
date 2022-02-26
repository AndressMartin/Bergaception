using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoCounter : MonoBehaviour
{
    [SerializeField] private IntEvent eventoCompletoComInt;

    #region Objetivo das Macas

    private int macasPegas = 0;

    public void ObjetivoMacas(int macas)
    {
        macasPegas += macas;

        ObjetivoMacasCompleto(macasPegas);
    }

    private void ObjetivoMacasCompleto(int macas)
    {
        eventoCompletoComInt.Raise(macas);
    }

    #endregion
}