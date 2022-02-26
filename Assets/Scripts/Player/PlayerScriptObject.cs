using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player")]
public class PlayerScriptObject : CharacterSO
{
    [SerializeField] private Arma arma;
    public int GetVidaMax => vidaMax;
    public float GetVelocidade => velocidade;
    public Arma GetArma => arma;
}
