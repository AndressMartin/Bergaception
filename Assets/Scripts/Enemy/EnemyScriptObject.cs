using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy")]

public class EnemyScriptObject : ScriptableObject
{
    [SerializeField] private int vida;
    [SerializeField] private float velocidade;
    [SerializeField] private bool patrulhaIdaVolta;
    [SerializeField] private Arma arma;
    [SerializeField] private AnimatorOverrideController animatorOverrideController;
    public int GetVida => vida;
    public float GetVelocidade => velocidade;
    public bool GetPatrulhaIdaVolta => patrulhaIdaVolta;
    public Arma GetArma => arma;
    public AnimatorOverrideController GetAnimatorOverrideController => animatorOverrideController;


}
