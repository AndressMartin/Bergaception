using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public int vida;
    public int vidaMax;

    public int dano;
    public bool morto;
    private int velocidade;
    private CharacterSO stats;
    private Animator animator;
    public UnityAction recebeuDano;
    public Weapon weapon;

    [SerializeField] public ItemColetavel item;
    public CharacterSO script;
    public virtual int Dano => dano;
    public int Vida => vida;
    public ItemColetavel Item => item;

    private void Awake()
    {
        if (GetComponent<BasicRigidBodyPush>() != null)
        {
            Init(script);
        }
    }
    public virtual void Init(CharacterSO stats)
    {
        if (GetComponent<BasicRigidBodyPush>() != null)
        {
            PlayerScriptObject _stats = (PlayerScriptObject)stats;

            vidaMax = _stats.GetVidaMax;
            vida = vidaMax;
            dano = _stats.GetArma.GetDano;

            if (animator != null)
            {
                animator = GetComponent<Animator>();
                if (animator.runtimeAnimatorController)
                    animator.runtimeAnimatorController = stats.animatorOverride;
            }
        }
    }
    public void InteracaoItem()
    {
        if (item == null)   //pegar Item
        {
            FindObjectOfType<Interacao>().Interagir();
        }
        else if (item != null) //Usar item
        {
            UsarItem();
        }
    }
    public void SoltarItem()
    {
        if (item != null)
        {
            DroparItem();
        }
    }
    public virtual void Morrer()
    {
        Debug.Log("objeto " + gameObject.name + "morto " + morto);
        GetComponent<Enemy>()?.MorrerA();
    }
    public virtual bool ReceberDano(int dano)
    {
        if (!morto)
        {
            vida -= dano;
            if (vida <= 0)
            {
                morto = true;
                Morrer();

            }
            recebeuDano?.Invoke();
        }
        return morto;
    }

    public virtual void ReceberKnockBack(Vector3 directionOfKnockback)
    {
        Quaternion rotation = Quaternion.LookRotation(directionOfKnockback, Vector3.up);
        transform.rotation = Quaternion.Inverse(rotation);
    }
    private void Update()
    {
        Debug.Log("tenho item " + item);
    }
    public void ToggleWeaponCollide()
    {
        weapon?.ToggleAttack();
    }
    public void ToggleWeaponCollideOff()
    {
        weapon?.ToggleAttackOff();
    }
    public void DroparItem()
    {
        if (item != null)
        {
            item.Dropar();
            item = null;
        }

        Debug.Log("Dropei o item");
    }

    public void PegueiItenChao(ItemColetavel _item)
    {
        Debug.Log("iten recebido "+_item);
        item = _item;
    }
    public void UsarItem()
    {
        item?.UsarItem();
    }

    public bool TemItem()
    {
        return item;
    }
}
