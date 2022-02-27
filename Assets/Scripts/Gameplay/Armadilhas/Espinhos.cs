using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Animator animator;

    private bool ativo;
    private float tempo;
    [SerializeField] private int dano;
    [SerializeField] private float tempoAtivo;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();

        boxCollider.enabled = false;
        ativo = false;
    }

    public void SubirEspinhos()
    {
        if(ativo == false)
        {
            boxCollider.enabled = true;
            ativo = true;
            animator.Play("Subir");
        }
    }

    public void SubirEDescerEspinhos()
    {
        if(ativo == false)
        {
            boxCollider.enabled = true;
            ativo = true;
            animator.Play("Subir");

            StartCoroutine(SubindoEDescendo());
        }
    }

    public void Desativar()
    {
        boxCollider.enabled = false;
        ativo = false;
    }

    private IEnumerator SubindoEDescendo()
    {
        tempo = 0;

        while(tempo < tempoAtivo)
        {
            tempo += Time.deltaTime;

            yield return null;
        }

        animator.Play("Descer");
    }

    private void OnTriggerEnter(Collider colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            colisao.GetComponent<Character>().ReceberDano(dano);
        }
    }
}
