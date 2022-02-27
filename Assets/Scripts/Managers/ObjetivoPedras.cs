using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoPedras : MonoBehaviour
{
    private bool ativou = false;
    [SerializeField] private VoidEvent contadorPedra;

    [SerializeField] private Light luz;

    private void OnTriggerEnter(Collider other)
    {
        if(ativou == true)
        {
            return;
        }

        if(other.CompareTag("ItemArremessado"))
        {
            ativou = true;
            contadorPedra.Raise();

            if(luz != null)
            {
                luz.color = Color.green;
            }
        }
    }
}
