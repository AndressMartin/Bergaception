using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{
    //Componentes
    private Mestre mestre;
    [SerializeField] private GameObject objeto;

    void Start()
    {
        mestre = FindObjectOfType<Mestre>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (input.)
        {
            mestre.AlterarPosicao(objeto, new Vector3(3, 1, 1));
        }
        */
    }
}
