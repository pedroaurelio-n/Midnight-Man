using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    public delegate void ItemAddedOnInventory(Item item);
    public static event ItemAddedOnInventory onItemAdded;

    [SerializeField] private bool setFaceDiretcion;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] [Range(1, 8)] private int facingDirection;

    [SerializeField] private List<TextboxSequence> sequences;

    [SerializeField] private bool willAddItem;
    [SerializeField] private Item item;

    public bool canInteract;
    private bool isPlayerInsideRange = false;

    private PlayerInputControls playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputControls();
    }

    private void Start()
    {
        playerInput.Gameplay.Interaction.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (canInteract && isPlayerInsideRange && GameManager.Instance.canMove && !GameManager.Instance.isTextboxOpen)
        {
            if (willAddItem && onItemAdded != null)
            {
                onItemAdded(item);
                canInteract = false;
            }

            if (setFaceDiretcion)
                animationController.ReceiveAnimationDirection(facingDirection);

            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(sequences[DataHolder.Instance.settings.Language_Id]);
        }
    }

    public void ActivateInteraction()
    {
        if (canInteract && isPlayerInsideRange && GameManager.Instance.canMove && !GameManager.Instance.isTextboxOpen)
        {
            if (willAddItem && onItemAdded != null)
            {
                onItemAdded(item);
                canInteract = false;
            }

            if (setFaceDiretcion)
                animationController.ReceiveAnimationDirection(facingDirection);

            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(sequences[DataHolder.Instance.settings.Language_Id]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;

        if (collision.TryGetComponent<Player>(out player))
        {
            isPlayerInsideRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player;

        if (collision.TryGetComponent<Player>(out player))
        {
            isPlayerInsideRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(sequences[DataHolder.Instance.settings.Language_Id]);
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
