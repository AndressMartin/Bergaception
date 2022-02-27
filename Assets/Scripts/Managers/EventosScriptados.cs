using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosScriptados : MonoBehaviour
{
    #region Objetivo Macas

    [SerializeField] private int macasQuePrecisamSerPegas;

    public void EventoMacas(int macas)
    {
        if(macas >= macasQuePrecisamSerPegas)
        {
            GetComponent<ObjetoInteragivel>().enabled = true;
        }
    }

    #endregion

    
}
