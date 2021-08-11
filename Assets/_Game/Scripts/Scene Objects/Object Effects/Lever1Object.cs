using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever1Object : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private InteractableObject lever;

    private void Action()
    {
        gate.SetActive(false);
        lever.canInteract = false;
    }

    private void OnEnable()
    {
        Lever1Event.onLever1Event += Action;
    }

    private void OnDisable()
    {
        Lever1Event.onLever1Event -= Action;
    }
}
