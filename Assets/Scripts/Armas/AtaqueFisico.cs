using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFisico : MonoBehaviour
{
    //Componentes
    private BoxCollider boxCollider;

    public void Iniciar()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
    public void Atacando(int dano)
    {
        boxCollider.enabled = true;
        if (Colisao.HitTest(boxCollider, FindObjectOfType<BasicRigidBodyPush>().transform.Find("HitBoxDano").GetComponent<BoxCollider>()))
        {
            HitTarget(dano);
        }
        boxCollider.enabled = false;
    }
    void HitTarget(int dano)
    {
        Debug.Log("Acertei o player");
    }

}
