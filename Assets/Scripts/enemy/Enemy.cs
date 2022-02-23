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
    AtaqueFisico ataqueFisico;
    Arma arma;


    [SerializeField] EnemyScriptObject enemyScriptObjectsManager;
    [SerializeField] GameObject player;

    //controle
    bool morto;
    int vidaMax;
    int vida;

    void Start()
    {
        morto = false;

        enemyMovement = GetComponent<EnemyMovement>();
        IA_enemy = GetComponent<IA_Enemy>();
        aiPath = GetComponent<AIPath>();
        ataqueFisico = GetComponentInChildren<AtaqueFisico>();

        arma = enemyScriptObjectsManager.GetArma;
        vidaMax = enemyScriptObjectsManager.GetVida;
        vida = vidaMax;

        enemyMovement.ReceberScriptObject(enemyScriptObjectsManager);
        enemyMovement.Inicar(this.gameObject,aiPath);

        IA_enemy.Inicar(enemyMovement,player,this);
        ataqueFisico.Iniciar();
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
        Debug.Log("animacao");
        ataqueFisico.Atacando(arma.GetDano);
    }

}
