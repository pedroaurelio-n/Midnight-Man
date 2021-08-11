using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float movementSpeed;

    [SerializeField] private float staminaMaxValue;
    [SerializeField] private float staminaRechargeRate;
    [SerializeField] private float staminaSpendRate;

    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private InventoryUI inventoryUI;

    public Inventory inventory;
    public bool hasCandle;

    private void Awake()
    {
        playerMovement.MovementSpeed = movementSpeed;

        playerMovement.StaminaMaxValue = staminaMaxValue;
        playerMovement.StaminaRechargeRate = staminaRechargeRate;
        playerMovement.StaminaSpendRate = staminaSpendRate;

        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
    }
}
