using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordNoteEvent : MonoBehaviour
{
    public delegate void PasswordNoteItemUsed(Item item);
    public static event PasswordNoteItemUsed onPasswordNoteAction;

    public Item Item;

    void Start()
    {
        if (onPasswordNoteAction != null && Item != null)
        {
            onPasswordNoteAction(Item);
        }
    }
}
