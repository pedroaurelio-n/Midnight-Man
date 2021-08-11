using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeNoteObject : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InteractableObject defaultInteraction;
    [SerializeField] private InteractableObject itemInteraction;
    [SerializeField] private List<TextboxSequence> failSequence;

    private bool isPlayerInsideRange;

    private void Action(Item item)
    {
        if (isPlayerInsideRange)
        {
            pauseMenu.InstantResumeGame();

            defaultInteraction.enabled = false;
            itemInteraction.enabled = true;
            itemInteraction.ActivateInteraction();

            inventoryUI.RemoveItem(item);
        }

        else
        {
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(failSequence[DataHolder.Instance.settings.Language_Id]);
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
        StrangeNoteEvent.onStrangeNoteAction += Action;        
    }

    private void OnDisable()
    {
        StrangeNoteEvent.onStrangeNoteAction -= Action;
    }
}
