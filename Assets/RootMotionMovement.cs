using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionMovement : MonoBehaviour
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

}
