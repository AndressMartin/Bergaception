using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int vida;
    private int vidaMax;
    public int dano;

    public void Init() 
    {
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReceberDano(int dano)
    {
        vida -= dano;
        return true;
    }
}
