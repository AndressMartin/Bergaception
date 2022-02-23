using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Enemy : MonoBehaviour
{
    //componentes
    EnemyMovement enemyMovement;
    Enemy enemy;
    EnemyAttackRange enemyAttackRange;
    EnemyVision enemyVision;
    GameObject objetoPlayer;

    //


    //Enuns
    public enum EstadoMovimentacao { Parado, IndoPlayer, RodeandoPlayer, Patrulhando }
    [SerializeField]private EstadoMovimentacao estadoMovimentacao;

    public enum EstadoAcoes { Atacando, FazerNada }
    [SerializeField] private EstadoAcoes estadoAcoes;
    public void Inicar(EnemyMovement _enemyMovement, GameObject _player,Enemy _enemy)
    {
        enemyMovement = _enemyMovement;
        objetoPlayer = _player;
        enemy = _enemy;

        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        enemyVision = GetComponentInChildren<EnemyVision>();
    }
    public void Main()
    {
        StateMachineDecisao();
        StateMachineAcao();
    }
    void StateMachineDecisao()
    {
        DecisaoMovimento();
        DecisaoAcoes();
    }
    void DecisaoMovimento()
    {

    }
    void DecisaoAcoes()
    {
        if(enemyVision.GetPlayerVision)
        {
            if (enemyAttackRange.GetPlayerZonaAtaque)
            {
                estadoAcoes = EstadoAcoes.Atacando;
                estadoMovimentacao = EstadoMovimentacao.RodeandoPlayer;
            }
            else
            {
                estadoAcoes = EstadoAcoes.FazerNada;
                estadoMovimentacao = EstadoMovimentacao.IndoPlayer;
            }
        }
        else
        {
            estadoAcoes = EstadoAcoes.FazerNada;
            estadoMovimentacao = EstadoMovimentacao.Patrulhando;
        }
        
    }
    void StateMachineAcao()
    {
        switch (estadoMovimentacao)
        {
            case EstadoMovimentacao.Parado:
                Parar();
                break;
            case EstadoMovimentacao.IndoPlayer:
                IrAtePlayer();
                break;
            case EstadoMovimentacao.RodeandoPlayer:
                RodearPlayer();
                break;
            case EstadoMovimentacao.Patrulhando:
                Patrulhar();
                break;

        }
        switch (estadoAcoes)
        {
            case EstadoAcoes.Atacando:
                Atacar();
                break;
            case EstadoAcoes.FazerNada:
                break;

        }
    }
   

    void Patrulhar()
    {
        if (ChegouNaDistancia(enemyMovement.GetListaPatrulha[enemyMovement.GetIndiceListaPatrulha].position))
        {
            enemyMovement.GerarNovoPontoOrdenado();
        }
        else
        {
            enemyMovement.Mover(enemyMovement.GetListaPatrulha[enemyMovement.GetIndiceListaPatrulha].position);
        }
    }
    void RodearPlayer()
    {
        Parar();
    }
    void Parar()
    {
        enemyMovement.Parar();
    }
    void IrAtePlayer()
    {
        if (!ChegouNaDistancia(objetoPlayer.transform.position))
        {
            enemyMovement.Mover(objetoPlayer.transform.position);
        }
        else
        {
            Parar();
        }

    }
    void Atacar()
    {
        enemy.Atacar();
    }
    bool ChegouNaDistancia(Vector3 _target)
    {
        if (Vector3.Distance(transform.position, _target) < 1)
        {
            return true;
        }
        return false;
    }
}
