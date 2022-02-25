using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    PontoPortal entrada;
    PontoPortal saida;

    [SerializeField]CharacterController player;
    private enum Sentido { MaoUnica, MaoDupla };
    [SerializeField] private Sentido sentido;

    public void ReceberEntrada(PontoPortal pontoPortal)
    {
        entrada = pontoPortal;
    }
    public void ReceberSaida(PontoPortal pontoPortal)
    {
        saida = pontoPortal;
    }

    public void Interagir()
    {
        if (entrada != null && saida != null)
        {
            switch (sentido)
            {
                case Sentido.MaoUnica:
                    if (entrada.GetColidindo)
                    {
                        player.transform.position = saida.transform.position;
                    }
                    break;
                case Sentido.MaoDupla:
                    if (entrada.GetColidindo)
                    {
                        player.transform.position = saida.transform.position;
                    }
                    else if (saida.GetColidindo)
                    {
                        player.transform.position = entrada.transform.position;
                    }
                    break;
            }
        }
    }

}
