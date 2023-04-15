using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput pInput;
    public PlayerInput.OnFootActions onFoot;
    public GameObject wholder;

    private PlayerMotor motor;
    private playerLook look;

    void Awake()
    {
        pInput = new PlayerInput();
        onFoot = pInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<playerLook>();

        onFoot.Movement.performed += ctx => motor.Walk();
        onFoot.Movement.canceled += ctx => motor.Swalk();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint(); 
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Attack.performed += ctx => motor.Attack();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}