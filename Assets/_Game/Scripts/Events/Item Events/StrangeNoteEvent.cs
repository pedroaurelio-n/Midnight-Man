using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeNoteEvent : MonoBehaviour
{
    public delegate void StrangeNoteItemUsed(Item item);
    public static event StrangeNoteItemUsed onStrangeNoteAction;

    public Item Item;

    void Start()
    {
        if (onStrangeNoteAction != null && Item != null)
        {
            onStrangeNoteAction(Item);
        }
    }
}
