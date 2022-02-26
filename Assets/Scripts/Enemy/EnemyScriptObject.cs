using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy")]

public class EnemyScriptObject : CharacterSO
{
    [SerializeField] private bool patrulhaIdaVolta;
    [SerializeField] private Arma arma;
    public int GetVidaMax => vidaMax;
    public float GetVelocidade => velocidade;
    public bool GetPatrulhaIdaVolta => patrulhaIdaVolta;
    public Arma GetArma => arma;

}
