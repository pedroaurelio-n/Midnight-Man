using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject panelInventory;
    [SerializeField] private GameObject txtNoItems;

    [SerializeField] private List<Button> mainPanelButtons;

    public void InventoryButtonAction()
    {

        var inventoryButtonList = inventoryUI.GetInventoryButtonList();

        if (inventoryButtonList.Count > 0)
        {
            foreach(Button button in mainPanelButtons)
            {
                button.interactable = false;
            }


            panelInventory.SetActive(true);
            pauseMenu.currentPanel = panelInventory;
            txtNoItems.SetActive(false);
            EventSystem.current.SetSelectedGameObject(inventoryButtonList[0].gameObject);
        }

        else
        {
            panelInventory.SetActive(true);
            pauseMenu.currentPanel = panelInventory;
            txtNoItems.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
