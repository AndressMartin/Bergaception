using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{
    //Componentes
    private Mestre mestre;
    [SerializeField] private GameObject objeto;

    private bool apertou = false;

    void Start()
    {
        mestre = FindObjectOfType<Mestre>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.K].wasPressedThisFrame && apertou == false)
        {
            mestre.MoverObjetoComTransicao(objeto, new Vector3(2, 2,5), 1);
            apertou = true;
        }
        
    }
}
