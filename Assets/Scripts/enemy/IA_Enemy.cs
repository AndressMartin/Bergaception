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

    public bool parar;

    private Vector3 posicaoOrigem;

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
        
        if(objetoPlayer == null)
        {
            objetoPlayer = FindObjectOfType<BasicRigidBodyPush>().gameObject;
        }
        

        posicaoOrigem = transform.position;

        AssignAnimationIDs();
        _animator = GetComponent<Animator>();
        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        enemyVision = GetComponentInChildren<EnemyVision>();
        if(enemyVision == null)
        {
            enemyVision = FindObjectOfType<EnemyVision>();
        }
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
        //Debug.Log(objetoPlayer);
        if(enemyVision.GetPlayerVision && !objetoPlayer.GetComponent<Character>().morto)
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
            if (enemyMovement.GetListaPatrulha.Count > 1)
            {
                enemyMovement.GerarNovoPontoOrdenado();
            }
            else
            {
                if (ChegouNaDistancia(posicaoOrigem))
                {
                    AnimcaoIdle();
                }
                else
                {
                    Mover(posicaoOrigem);
                }
            }
        }
        else
        {
            Mover(enemyMovement.GetListaPatrulha[enemyMovement.GetIndiceListaPatrulha].position);
        }
    }
    void AnimcaoIdle()
    {
        _animator.SetFloat(_animIDSpeed, 0);

    }
    void RodearPlayer()
    {
        Parar();
    }
    void Parar()
    {
        _animationBlend = 0;
        _animator.SetFloat(_animIDSpeed, _animationBlend);
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


        _animator.SetBool(_animIDAttack, true);
        enemy.Atacar();
    }
    void Mover(Vector3 _target)
    {
        _animationBlend = 1;
        _animator.SetFloat(_animIDSpeed, _animationBlend);
        enemyMovement.Mover(_target);

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
