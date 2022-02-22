using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject targer;
    private AIPath aiPath;

    [SerializeField]private float velocidade;
    public void Inicar(GameObject _target,AIPath _aIPath)
    {
        targer = _target;
        aiPath = _aIPath;    
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
}
