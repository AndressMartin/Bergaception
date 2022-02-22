using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //componente
    EnemyMovement enemyMovement;
    IA_Enemy IA_enemy;
    AIPath aiPath;

    [SerializeField]GameObject player;

    //controle
    bool morto;

    void Start()
    {
        morto = false;

        enemyMovement = GetComponent<EnemyMovement>();
        IA_enemy = GetComponent<IA_Enemy>();
        aiPath = GetComponent<AIPath>();

        enemyMovement.Inicar(this.gameObject,aiPath);
        IA_enemy.Inicar(enemyMovement,player,this);
    }

    void Update()
    {
        if (!morto)
        {
            IA_enemy.Main();
        }
    }
    public void Atacar()
    {
        Debug.Log("atacando");
    }
}
