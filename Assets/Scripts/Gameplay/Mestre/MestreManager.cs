using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MestreManager : MonoBehaviour
{
    private Mestre mestre;

    private void Start()
    {
        mestre = GetComponent<Mestre>();
    }

    public void EfeitoTurnBlack()
    {
        mestre.CriarEfeito(Mestre.Efeito.Apagao);
    }
}
