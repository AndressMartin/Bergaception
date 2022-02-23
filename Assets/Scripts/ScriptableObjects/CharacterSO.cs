using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Characters")]
public class CharacterSO : ScriptableObject
{
    public int vida;
    public int vidaMax;
    public int dano;
    public int velocidade;

    public AnimatorOverrideController animatorOverride;
}
