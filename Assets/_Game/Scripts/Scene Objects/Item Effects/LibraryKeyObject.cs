using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKeyObject : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private List<TextboxSequence> successSequence;
    [SerializeField] private List<TextboxSequence> failSequence;

    private bool isPlayerInsideRange;

    private void Action(Item item)
    {
        if (isPlayerInsideRange)
        {
            pauseMenu.InstantResumeGame();
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(successSequence[DataHolder.Instance.settings.Language_Id]);

            gameObject.GetComponent<InteractableObject>().enabled = false;
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            door.canTeleportPlayer = true;

            //inventoryUI.RemoveItem(item);
        }

        else
        {
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(failSequence[DataHolder.Instance.settings.Language_Id]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            AudioManager.Instance.PlayAudio(3);
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
        LibraryKeyEvent.onLibraryKeyAction += Action;
    }

    private void OnDisable()
    {
        LibraryKeyEvent.onLibraryKeyAction -= Action;
    }
}
