using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningKeyEvent : MonoBehaviour
{
    public delegate void DiningKeyItemUsed(Item item);
    public static event DiningKeyItemUsed onDiningKeyAction;

    public Item Item;

    void Start()
    {
        if (onDiningKeyAction != null && Item != null)
        {
            onDiningKeyAction(Item);
        }
    }
}
