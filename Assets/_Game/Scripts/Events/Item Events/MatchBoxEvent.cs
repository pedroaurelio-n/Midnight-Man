using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBoxEvent : MonoBehaviour
{
    public delegate void MatchBoxItemUsed(Item item);
    public static event MatchBoxItemUsed onMatchBoxAction;

    public Item Item;

    void Start()
    {
        if (onMatchBoxAction != null && Item != null)
        {
            onMatchBoxAction(Item);
        }
    }
}
