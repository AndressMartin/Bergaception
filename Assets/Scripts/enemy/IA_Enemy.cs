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
    Animator _animator;

    //Variaveis
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDAttack;
    private int _animIDReaction;
    private int _animIDFreeFall;
    private int _animIDDying;

    bool subindo;
    bool descendo;

    [SerializeField]private float _animationBlend;

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

        AssignAnimationIDs();
        _animator = GetComponent<Animator>();
        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        enemyVision = GetComponentInChildren<EnemyVision>();
    }
    public void Main()
    {
        StateMachineDecisao();
        StateMachineAcao();
    }
    public void Morrer()
    {
        Parar();
        
        _animator.SetBool(_animIDDying, true);
    }
    void StateMachineDecisao()
    {
        DecisaoAcoes();
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
            Mover(enemyMovement.GetListaPatrulha[enemyMovement.GetIndiceListaPatrulha].position);
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
            Mover(objetoPlayer.transform.position);
        }
        else
        {
            Parar();
        }

    }
    void Atacar()
    {
        if(!ChegouNaDistancia(objetoPlayer.transform.position))
        {
            transform.LookAt(objetoPlayer.transform);
        }

        _animator.SetBool(_animIDAttack, true);
        enemy.Atacar();
    }
    void Mover(Vector3 _target)
    {
        SomarNumero();
        _animator.SetFloat(_animIDSpeed, _animationBlend);
        enemyMovement.Mover(_target);

    }
    void SomarNumero()
    {
        if(subindo)
        {
            _animationBlend += 0.1F;
        }
        if(descendo)
        {
            _animationBlend -= 0.1F;

        }

        if (_animationBlend >= 6)
        {
            subindo = false;
            descendo = true;
        }
        if(_animationBlend <= 0)
        {
            subindo = true;
            descendo = false;
        }

        
    }
    bool ChegouNaDistancia(Vector3 _target)
    {
        if (Vector3.Distance(transform.position, _target) < 0.5)
        {
            return true;
        }
        return false;
    }


    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDAttack = Animator.StringToHash("Attacking");
        _animIDReaction = Animator.StringToHash("Reaction");
        _animIDDying = Animator.StringToHash("Dying");
    }
    public void AllBoolFalse()
    {
        _animator.SetBool(_animIDJump, false);
        _animator.SetBool(_animIDAttack, false);
    }
}
