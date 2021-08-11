using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public delegate void PlayerStaminaChanged(float maxStamina, float newStamina, bool isSpendingStamina);
    public static event PlayerStaminaChanged onStaminaChange;


    private float _movementSpeed;
    public float MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }


    private float _staminaMaxValue;
    public float StaminaMaxValue
    {
        get { return _staminaMaxValue; }
        set { _staminaMaxValue = value; }
    }

    private float _staminaRechargeRate;
    public float StaminaRechargeRate
    {
        get { return _staminaRechargeRate; }
        set { _staminaRechargeRate = value; }
    }

    private float _staminaSpendRate;
    public float StaminaSpendRate
    {
        get { return _staminaSpendRate; }
        set { _staminaSpendRate = value; }
    }


    private float _stamina;

    private float _walkSpeed;
    private float _runSpeed;

    private Vector2 _movementDirection;

    private float runValue;

    private bool canRun;


    private PlayerInputControls playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = new PlayerInputControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput.Gameplay.Movement.performed += Movement;

        playerInput.Gameplay.Run.performed += Run;
        playerInput.Gameplay.Run.canceled += Run;

        canRun = true;

        _stamina = _staminaMaxValue;

        _walkSpeed = _movementSpeed;
        _runSpeed = _movementSpeed * 1.7f;
    }

    private void Movement(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.canMove)
        {
            _movementDirection = ctx.ReadValue<Vector2>();
        }
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        runValue = ctx.ReadValue<float>();
    }

    private void Update()
    {
        if (!GameManager.Instance.canMove)
        {
            _movementDirection = Vector2.zero;
            return;
        }

        CheckRunning();
    }

    private void FixedUpdate()
    {
        rb.AddForce(_movementSpeed * _movementDirection.normalized, ForceMode2D.Force);
    }

    private void CheckRunning()
    {
        bool isRunning = canRun && runValue != 0 && _stamina > 0 && _movementDirection != Vector2.zero;

        if (isRunning)
        {
            _stamina -= Time.deltaTime * _staminaSpendRate;
            _movementSpeed = _runSpeed;

            if (onStaminaChange != null)
                onStaminaChange(_staminaMaxValue, _stamina, true);
            return;
        }

        if (_stamina < _staminaMaxValue)
        {
            _stamina += Time.deltaTime * _staminaRechargeRate;
            _movementSpeed = _walkSpeed;

            if (onStaminaChange != null)
                onStaminaChange(_staminaMaxValue, _stamina, false);
        }
    }

    public Vector2 GetMovementDirection()
    {
        return _movementDirection;
    }

    #region Enable/Disable
    private void OnEnable()
    {
        playerInput.Enable();
        StaminaUI.onZeroStamina += () => canRun = !canRun;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        StaminaUI.onZeroStamina -= () => canRun = !canRun;
    }
    #endregion
}