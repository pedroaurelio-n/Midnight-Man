using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private List<TextboxSequence> dialog;

    [SerializeField] private GameObject txtNoItems;

    public GameObject currentPanel;

    public GameObject panelMain;
    public GameObject panelInventory;
    public GameObject panelLoad;
    public GameObject panelSave;
    public GameObject panelItem;

    [SerializeField] private List<Button> mainPanelButtons;

    private PlayerInputControls playerInput;

    private GameObject tempInventoryItemButton;

    private void Awake()
    {
        playerInput = new PlayerInputControls();
    }

    private void Start()
    {
        playerInput.Gameplay.PauseGame.performed += PauseControl;
        currentPanel = panelMain;

        fadeImage.DOFade(1f, 0f).SetUpdate(true);

        fadeImage.DOFade(0f, 3f).SetUpdate(true).OnComplete(delegate {
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(dialog[DataHolder.Instance.settings.Language_Id]);
        });
    }

    private void PauseControl(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.isTextboxOpen)
            return;

        if (!GameManager.Instance.isGamePaused)
        {
            panelMain.SetActive(true);
            EventSystem.current.SetSelectedGameObject(mainPanelButtons[0].gameObject);

            PauseGame();
        }

        else
        {
            TryResumeGame();

            if (currentPanel == panelInventory)
            {
                foreach (Button button in mainPanelButtons)
                {
                    button.interactable = true;
                }

                txtNoItems.SetActive(false);
                EventSystem.current.SetSelectedGameObject(mainPanelButtons[0].gameObject);
                panelInventory.SetActive(false);
                currentPanel = panelMain;
            }

            else if (currentPanel == panelLoad)
            {
                foreach (Button button in mainPanelButtons)
                {
                    button.interactable = true;
                }

                EventSystem.current.SetSelectedGameObject(mainPanelButtons[1].gameObject);
                panelLoad.SetActive(false);
                currentPanel = panelMain;
            }

            else if (currentPanel == panelSave)
            {
                foreach (Button button in mainPanelButtons)
                {
                    button.interactable = true;
                }

                EventSystem.current.SetSelectedGameObject(mainPanelButtons[2].gameObject);
                panelSave.SetActive(false);
                currentPanel = panelMain;
            }

            else if (currentPanel == panelItem)
            {
                EventSystem.current.SetSelectedGameObject(tempInventoryItemButton);
                panelItem.SetActive(false);
                currentPanel = panelInventory;
            }
        }
    }

    private void ItemPanelChecker(GameObject itemObject, Button itemButton, Item item)
    {
        currentPanel = panelItem;
        panelItem.SetActive(true);
        tempInventoryItemButton = itemObject;

        panelItem.GetComponent<ItemPanelUpdater>().UpdateInfo(item);
    }

    private void PauseGame()
    {
        GameManager.Instance.isGamePaused = true;
        GameManager.Instance.canMove = false;

        Time.timeScale = 0f;
    }

    private void TryResumeGame()
    {
        if (currentPanel != panelMain)
            return;

        Time.timeScale = 1f;

        GameManager.Instance.isGamePaused = false;
        GameManager.Instance.canMove = true;

        EventSystem.current.SetSelectedGameObject(null);
        AudioManager.Instance.PlayAudio(2);
        panelMain.SetActive(false);
    }

    public void InstantResumeGame()
    {
        Time.timeScale = 1f;

        GameManager.Instance.isGamePaused = false;
        GameManager.Instance.canMove = true;

        EventSystem.current.SetSelectedGameObject(null);
        currentPanel = panelMain;

        foreach (Button button in mainPanelButtons)
        {
            button.interactable = true;
        }

        panelMain.SetActive(false);
        panelInventory.SetActive(false);
        panelLoad.SetActive(false);
        panelSave.SetActive(false);
        panelItem.SetActive(false);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(1);
    }

    #region Enable/Disable
    private void OnEnable()
    {
        playerInput.Enable();
        ItemTemplateInfo.onItemActivation += ItemPanelChecker;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        ItemTemplateInfo.onItemActivation -= ItemPanelChecker;
    }
    #endregion
}
