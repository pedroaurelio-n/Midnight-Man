using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageKeyEvent : MonoBehaviour
{
    public delegate void GarageKeyItemUsed(Item item);
    public static event GarageKeyItemUsed onGarageKeyAction;

    public Item Item;

    void Start()
    {
        if (onGarageKeyAction != null && Item != null)
        {
            onGarageKeyAction(Item);
        }
    }
}
