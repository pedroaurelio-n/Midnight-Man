using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1Object : MonoBehaviour
{
    [SerializeField] private Door playerTeleport;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private TextboxSequence successSequence;
    [SerializeField] private TextboxSequence failSequence;

    private bool isPlayerInsideRange;

    private void Action(Item item)
    {
        if (isPlayerInsideRange)
        {
            pauseMenu.InstantResumeGame();
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(successSequence);

            gameObject.GetComponent<InteractableObject>().enabled = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            playerTeleport.canTeleportPlayer = true;

            inventoryUI.RemoveItem(item);
        }

        else
        {
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(failSequence);
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

    private void OnEnable()
    {
        Key1Event.onKey1Action += Action;
    }

    private void OnDisable()
    {
        Key1Event.onKey1Action -= Action;
    }
}
