using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKeyEvent : MonoBehaviour
{
    public delegate void LibraryKeyItemUsed(Item item);
    public static event LibraryKeyItemUsed onLibraryKeyAction;

    public Item Item;

    void Start()
    {
        if (onLibraryKeyAction != null && Item != null)
        {
            onLibraryKeyAction(Item);
        }
    }
}
