using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Enemy : MonoBehaviour
{
    //componentes
    EnemyMovement enemyMovement;
    GameObject objetoPlayer;

    public enum EstadoMovimentacao { Parado, IndoPlayer, RodeandoPlayer, Patrulhando }
    [SerializeField]private EstadoMovimentacao estadoMovimentacao;
    public void Inicar(EnemyMovement _enemyMovement, GameObject _player)
    {
        enemyMovement = _enemyMovement;
        objetoPlayer = _player;
    }
    public void Main()
    {
        StateMachine();
    }
    void StateMachine()
    {
        switch (estadoMovimentacao)
        {
            case EstadoMovimentacao.Parado:
                break;
            case EstadoMovimentacao.IndoPlayer:
                IrAtePlayer();
                break;
            case EstadoMovimentacao.RodeandoPlayer:
                break;
            case EstadoMovimentacao.Patrulhando:
                break;
            default:
                break;
        }
    }
    void IrAtePlayer()
    {
        enemyMovement.Mover(objetoPlayer.transform.position);
    }
}
