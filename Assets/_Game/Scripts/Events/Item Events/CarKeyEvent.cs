using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKeyEvent : MonoBehaviour
{
    public delegate void CarKeyItemUsed(Item item);
    public static event CarKeyItemUsed onCarKeyAction;

    public Item Item;

    void Start()
    {
        if (onCarKeyAction != null && Item != null)
        {
            onCarKeyAction(Item);
        }
    }
}
