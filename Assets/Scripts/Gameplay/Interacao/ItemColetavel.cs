using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    protected Character character;
    public bool consumivel;
    public int quantidadeUsos;
    private void Start()
    {
        character = FindObjectOfType<Character>();
    }
    public virtual void PegarItem()
    {
        character.PegueiItenChao(this);
        gameObject.SetActive(false);
    }
    public virtual void UsarItem()
    {
        Debug.Log("Usar item");
        if (consumivel)
        {
            quantidadeUsos--;

            if (quantidadeUsos <= 0)
            {
                character.PegueiItenChao(null);//perder referencia
            }
        }
    }
    public virtual void Dropar()
    {
        Debug.Log("soltar item");
        gameObject.SetActive(true);
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 10, character.transform.position.z);
    }
}
