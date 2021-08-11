using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuButton : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject panel;
    [SerializeField] private Button buttonNext;

    [SerializeField] private List<Button> mainPanelButtons;

    public void PauseButtonAction()
    {
        foreach (Button button in mainPanelButtons)
        {
            button.interactable = false;
        }

        panel.SetActive(true);
        pauseMenu.currentPanel = panel;
        if (buttonNext != null)
            EventSystem.current.SetSelectedGameObject(buttonNext.gameObject);
    }
}
