using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacao : MonoBehaviour
{
    private HUD hud;

    private BoxCollider boxCollider;
    private Character player;
    private ObjetoInteragivel objetoAtual;

    [SerializeField] private LayerMask m_LayerMask;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();

        boxCollider = GetComponent<BoxCollider>();
        player = GetComponentInParent<Character>();
        objetoAtual = null;

        StartCoroutine(ProcurarObjetoInteragivel());
    }

    private void FixedUpdate()
    {
        if(objetoAtual != null)
        {
            hud.AtualizarIconeInteracao(true, objetoAtual.BoxCollider.bounds.center + new Vector3(0, objetoAtual.BoxCollider.bounds.extents.y, 0));
        }
    }

    public void Interagir()
    {
        objetoAtual?.Interagir();
    }

    private IEnumerator ProcurarObjetoInteragivel()
    {
        while(true)
        {
            objetoAtual = null;

            if(player.Item == null)
            {
                Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity, m_LayerMask);

                foreach (Collider objeto in hitColliders)
                {
                    ObjetoInteragivel objetoInteragivel = objeto.GetComponent<ObjetoInteragivel>();

                    if (objetoInteragivel != null)
                    {
                        if (objetoInteragivel.Ativo == false)
                        {
                            continue;
                        }

                        objetoAtual = objetoInteragivel;
                        break;
                    }
                }
            }

            if(objetoAtual == null)
            {
                hud.AtualizarIconeInteracao(false, Vector3.zero);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}