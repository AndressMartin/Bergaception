using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private RectTransform iconeInteracao;

    [SerializeField] private RectTransform[] coracoes;
    [SerializeField] private GameObject[] armas;
    [SerializeField] private GameObject[] itens;

    private void Start()
    {
        TrocarIconeArma(1);
        TrocarIconeItem(0);
    }

    public void AtualizarHP(int hp)
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if(i < hp)
            {
                coracoes[i].gameObject.SetActive(true);
            }
            else
            {
                coracoes[i].gameObject.SetActive(false);
            }
        }
    }

    public void TrocarIconeArma(int id)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            if (i == id)
            {
                armas[i].SetActive(true);
            }
            else
            {
                armas[i].SetActive(false);
            }
        }
    }

    public void TrocarIconeItem(int id)
    {
        for (int i = 0; i < itens.Length; i++)
        {
            if (i == id)
            {
                itens[i].SetActive(true);
            }
            else
            {
                itens[i].SetActive(false);
            }
        }
    }

    public void AtualizarIconeInteracao(bool ativo, Vector3 posicao)
    {
        iconeInteracao.gameObject.SetActive(ativo);

        if (ativo == false)
        {
            return;
        }

        iconeInteracao.position = cam.WorldToScreenPoint(posicao);
    }
}
