using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //componente
    EnemyMovement enemyMovement;
    IA_Enemy IA_enemy;
    AIPath aiPath;
    [SerializeField] GameObject player;

    //controle

    public override void Init(CharacterSO stats)
    {
        EnemyScriptObject _stats = (EnemyScriptObject)stats;

        vidaMax = _stats.GetVidaMax;
        vida = vidaMax;
        dano = _stats.GetArma.GetDano;
    }

    void Start()
    {
        Init(script);
        enemyMovement = GetComponent<EnemyMovement>();
        IA_enemy = GetComponent<IA_Enemy>();
        aiPath = GetComponent<AIPath>();
        
        player = FindObjectOfType<Character>().gameObject;

        enemyMovement.ReceberScriptObject((EnemyScriptObject)script);
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
        //ataqueFisico.Atacando(arma.GetDano);
    }
    public void MorrerA()
    {
        morto = true;
        IA_enemy.Morrer();
    }


}
