using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionMovement : Character
{
    Animator animator;
    CharacterController cc;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        cc.Move(velocity);
    }

    public override bool ReceberDano(int dano)
    {
        animator.SetTrigger("Reaction");
        return base.ReceberDano(dano);
    }
}
