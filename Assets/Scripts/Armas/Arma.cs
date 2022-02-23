using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "itens/Arma")]

public class Arma : ScriptableObject
{
    [SerializeField] private string nome;
    [SerializeField] private int dano;

    public int GetDano => dano;
}
