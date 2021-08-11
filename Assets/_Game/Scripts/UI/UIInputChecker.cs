using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputChecker : MonoBehaviour
{
    private EventSystem eventSystem;

    private GameObject lastSelectedGameObject;

    private PlayerInputControls playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputControls();
    }

    private void Start()
    {
        //playerInput.Gameplay.PauseGame.performed += PauseControl;

        eventSystem = EventSystem.current;
    }

    //private void Update()
    //{
    //    var currentSelectedGameObject = eventSystem.currentSelectedGameObject;

    //    if (currentSelectedGameObject == lastSelectedGameObject)
    //        return;

    //    print("button change");
    //    lastSelectedGameObject = currentSelectedGameObject;
    //}

    public void Submit()
    {
        print("submit");
    }

    public void Cancel()
    {
        print("cancel");
    }
    
    public void Move()
    {
        print("button change");
    }
}
