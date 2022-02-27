using StarterAssets;
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
    protected Animator animator;
    public UnityAction recebeuDano;
    public Weapon weapon;
    private Interacao interacao;

    private bool personagem = false;

    private HUD hud;

    [SerializeField] private ItemColetavel item;

    public CharacterSO script;
    public virtual int Dano => dano;
    public int Vida => vida;
    public ItemColetavel Item => item;
    public HUD Hud => hud;

    private void Awake()
    {
        if (GetComponent<BasicRigidBodyPush>() != null)
        {
            Init(script);
        }
    }

    private void Start()
    {
        item = null;
        interacao = GetComponentInChildren<Interacao>();
        hud = FindObjectOfType<HUD>();
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

            personagem = true;
        }
    }
    public void InteracaoItem()
    {
        if (item == null)   //pegar Item
        {
            interacao.Interagir();
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
        ZerarBoleanosAnimcao();
        GetComponent<Enemy>()?.MorrerA();
        ToggleWeaponCollideOff();
        FindObjectOfType<AudioManager>().Play("Dead");
    }
    public virtual bool ReceberDano(int dano)
    {
        if (GetComponent<Enemy>())
        {
            GetComponent<Enemy>().TomarDAno();
        }
        else
        {
            if (!morto)
            {
                vida -= dano;

                if (personagem == true)
                {
                    hud.AtualizarHP(vida);
                }

                if (vida <= 0)
                {
                    morto = true;
                    Morrer();

                }
                else
                {
                    ZerarBoleanosAnimcao();
                    recebeuDano?.Invoke();
                    ToggleWeaponCollideOff();
                }
            }
        }
        return morto;
    }
    public virtual void ZerarBoleanosAnimcao()
    {
        GetComponent<ThirdPersonController>()?.AllBoolFalse();
    }

    public virtual void ReceberKnockBack(Vector3 directionOfKnockback)
    {
        Quaternion rotation = Quaternion.LookRotation(directionOfKnockback, Vector3.up);
        transform.rotation = Quaternion.Inverse(rotation);
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

            if(personagem == true)
            {
                hud.TrocarIconeItem(0);
            }
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
