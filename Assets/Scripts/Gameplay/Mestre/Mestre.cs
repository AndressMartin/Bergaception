using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mestre : Singleton<Mestre>
{
    //Eventos
    [SerializeField] private VoidEvent comandosHabilitados;
    [SerializeField] private VoidEvent andarDesabilitado;
    [SerializeField] private VoidEvent correrDesabilitado;
    [SerializeField] private VoidEvent puloDesabilitado;
    [SerializeField] private VoidEvent ataqueDesabilitado;

    [SerializeField] private VoidEvent naoCausarDano;
    [SerializeField] private VoidEvent lentidao;
    [SerializeField] private VoidEvent semAr;
    [SerializeField] private VoidEvent semVisao;
    [SerializeField] private VoidEvent criarEspinhos;
    [SerializeField] private VoidEvent criarInimigos;
    [SerializeField] private VoidEvent superInimigos;

    //Enuns
    public enum ComandoJogador { Andar, Correr, Pular, Atacar };
    public enum Efeito { NaoCausarDano, Lentidao, SemAr, SemVisao, CriarEspinhos, CriarInimigos, SuperInimigos };

    public void HabilitarComandosJogador()
    {
        comandosHabilitados.Raise();
    }

    public void DesativarComandoJogador(ComandoJogador comando)
    {
        switch(comando)
        {
            case ComandoJogador.Andar:
                andarDesabilitado.Raise();
                break;

            case ComandoJogador.Correr:
                correrDesabilitado.Raise();
                break;

            case ComandoJogador.Pular:
                puloDesabilitado.Raise();
                break;

            case ComandoJogador.Atacar:
                ataqueDesabilitado.Raise();
                break;
        }
    }

    public void CriarEfeito(Efeito efeito)
    {
        switch(efeito)
        {
            case Efeito.NaoCausarDano:
                naoCausarDano.Raise();
                break;

            case Efeito.Lentidao:
                lentidao.Raise();
                break;

            case Efeito.SemAr:
                semAr.Raise();
                break;

            case Efeito.SemVisao:
                semVisao.Raise();
                break;

            case Efeito.CriarEspinhos:
                criarEspinhos.Raise();
                break;

            case Efeito.CriarInimigos:
                criarInimigos.Raise();
                break;

            case Efeito.SuperInimigos:
                superInimigos.Raise();
                break;
        }
    }

    public void CriarEfeitoAleatorio(Efeito[] efeitos)
    {
        if(efeitos.Length <= 0)
        {
            return;
        }

        int indice = Random.Range(0, efeitos.Length);

        CriarEfeito(efeitos[indice]);
    }

    public void MoverObjeto(GameObject objeto, Vector3 novaPosicao)
    {
        objeto.transform.position = novaPosicao;
    }

    public void MoverObjetoComTransicao(GameObject objeto, Vector3 novaPosicao, float velocidade)
    {
        StartCoroutine(MoverObjetoCorrotina(objeto, novaPosicao, velocidade));
    }

    private IEnumerator MoverObjetoCorrotina(GameObject objeto, Vector3 novaPosicao, float velocidade)
    {
        bool usaRigidbody = false;
        Rigidbody rb = null;

        if(objeto.GetComponent<Rigidbody>())
        {
            rb = objeto.GetComponent<Rigidbody>();

            if(rb.isKinematic == false)
            {
                rb.isKinematic = true;
                usaRigidbody = true;
            }
        }

        while (objeto.transform.position != novaPosicao)
        {
            objeto.transform.position = Vector3.MoveTowards(objeto.transform.position, novaPosicao, velocidade * Time.deltaTime);

            yield return null;
        }

        if(usaRigidbody == true)
        {
            rb.isKinematic = false;
        }
    }
}
