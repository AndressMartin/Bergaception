using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacao : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private LayerMask m_LayerMask;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    } 
    public void Interagir()
    {

        Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, m_LayerMask);

        foreach(Collider objeto in hitColliders)
        {
            ObjetoInteragivel objetoInteragivel = objeto.GetComponent<ObjetoInteragivel>();

            if(objetoInteragivel != null)
            {
                if(objetoInteragivel.Ativo == false)
                {
                    continue;
                }

                objetoInteragivel.Interagir();
                break;
            }
        }
    }
}