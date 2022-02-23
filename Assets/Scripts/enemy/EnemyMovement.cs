using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //componentes
    private GameObject targer;
    private AIPath aiPath;
    private EnemyScriptObject enemyScriptObjectsManager;

    //variaveis

    private float velocidade;
    [SerializeField] private List<Transform> targetsPatrulha = new List<Transform>();

    //variavelControle
    private int indiceListaPatrulha;
    private bool patrulhaIdaVolta;

    public List<Transform> GetListaPatrulha => targetsPatrulha;
    public int GetIndiceListaPatrulha => indiceListaPatrulha;
    public void Inicar(GameObject _target,AIPath _aIPath)
    {
        targer = _target;
        aiPath = _aIPath;
        VerificarPontoMaisPertoRota();
        RotaIdaVolta();
    }
    public void ReceberScriptObject(EnemyScriptObject _enemyScriptObject)
    {
        enemyScriptObjectsManager = _enemyScriptObject;
        velocidade = enemyScriptObjectsManager.GetVelocidade;
        patrulhaIdaVolta = enemyScriptObjectsManager.GetPatrulhaIdaVolta;
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
    void VerificarPontoMaisPertoRota()
    {
        int ponto0 = 0;
        float menorPonto = 50;

        for (int i = 0; i < targetsPatrulha.Count; i++)
        {
            float distancia = Vector3.Distance(transform.position, targetsPatrulha[i].transform.position);
            if (distancia <= menorPonto)
            {
                ponto0 = i;
                menorPonto = distancia;
            }
        }
        indiceListaPatrulha = ponto0;
    }
}
