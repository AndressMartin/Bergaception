using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{
    //Componentes
    private Mestre mestre;

    private bool apertou = false;

    void Start()
    {
        mestre = FindObjectOfType<Mestre>();
        mestre.CriarEfeito(Mestre.Efeito.SemVisao);
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current[Key.K].wasPressedThisFrame && apertou == false)
        {
            apertou = true;

            mestre.DesligarEfeitos();
        }
    }
}
