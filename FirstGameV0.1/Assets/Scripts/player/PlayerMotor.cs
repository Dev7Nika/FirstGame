using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public playerAnimation animator;
    public Camera cam;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public LayerMask mask;
    public float hitDistance = 3f;
    public float speed = 5f;
    public bool IsGrounded;
    private bool crouching;
    private bool sprinting;
    private bool walking;
    private bool lerpCrouch;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer;
    public bool canAttack;
    public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        canJump = true;
        sprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }

            //animator.Idle();
        }
    }
    //recieve the inputs from InputManager.cs and apply them to the character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (IsGrounded && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
            animator.Jump();
            canJump = false;
        }
        Invoke("StopJump", 0.1f);
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8;
            animator.Run();
        }
        else if (!sprinting && walking)
        {
            Walk();
        }
    }

    public void Walk()
    {
        Debug.Log("walking");
        if (!sprinting)
        {
            speed = 5f;
            animator.Walk();
        }
    }
    public void Swalk()
    {
        sprinting = false;
        animator.Idle();
    }
    public void Attack()
    {
        if (canAttack)
        {
            Debug.Log("Attacked!");
        }
    }

    public void StopJump()
    {
        if (IsGrounded == false)
        {
            Invoke("StopJump", 0.01f);
            Debug.Log("stopjump performed");
        }
        else
        {
            animator.sJump();
            canJump = true;
        }
    }
}
