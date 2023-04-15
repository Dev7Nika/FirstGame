using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        Debug.Log("idle");
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
    }

    public void Walk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
    }

    public void Run()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
    }

    public void Jump()
    {
        Debug.Log("jumped");
        animator.SetBool("isJumping", true);
    }
    public void sJump()
    {
        Debug.Log("stopped");
        animator.SetBool("isJumping", false);
    }
}
