using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //componentes
    private GameObject targer;
    private AIPath aiPath;

    //variaveis

    [SerializeField] private float velocidade;
    [SerializeField] private List<Transform> targetsPatrulha = new List<Transform>();

    //variavelControle
    [SerializeField] private int indiceListaPatrulha;
    [SerializeField] private bool patrulhaIdaVolta;

    public List<Transform> GetListaPatrulha => targetsPatrulha;
    public int GetIndiceListaPatrulha => indiceListaPatrulha;
    public void Inicar(GameObject _target,AIPath _aIPath)
    {
        targer = _target;
        aiPath = _aIPath;
        RotaIdaVolta();
    }

   
    public void Mover(Vector3 _target)
    {
        aiPath.canMove = true;
        aiPath.maxSpeed = velocidade;
        aiPath.destination = _target;
    }
    public void Parar()
    {
        aiPath.canMove = false;
        aiPath.maxSpeed = 0;
        aiPath.destination = transform.position;
    }
   
    public void GerarNovoPontoOrdenado()
    {
        if(indiceListaPatrulha < targetsPatrulha.Count -1)
        {
            indiceListaPatrulha++;
        }
        else if(indiceListaPatrulha >= targetsPatrulha.Count -1)
        {
            indiceListaPatrulha = 0;
        }
    }
    void RotaIdaVolta()
    {
        if (patrulhaIdaVolta)
        {
            int valor = targetsPatrulha.Count;
            for (int i = 0; i < valor; i++)
            {
                if (i != valor + 1)
                {
                    targetsPatrulha.Add(targetsPatrulha[valor - i - 1]);
                }
            }
        }
    }
}
