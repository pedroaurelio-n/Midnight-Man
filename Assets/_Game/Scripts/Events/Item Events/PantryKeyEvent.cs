using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryKeyEvent : MonoBehaviour
{
    public delegate void PantryKeyItemUsed(Item item);
    public static event PantryKeyItemUsed onPantryKeyAction;

    public Item Item;

    void Start()
    {
        if (onPantryKeyAction != null && Item != null)
        {
            onPantryKeyAction(Item);
        }
    }
}
