using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    protected virtual int Id => 0;

    protected Character character;
    public bool consumivel;
    public int quantidadeUsos;
    private void Start()
    {
        character = FindObjectOfType<BasicRigidBodyPush>().GetComponent<Character>();
    }
    public virtual void PegarItem()
    {
        character.PegueiItenChao(this);
        gameObject.SetActive(false);
        character.Hud.TrocarIconeItem(Id);
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
                character.Hud.TrocarIconeItem(0);
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
