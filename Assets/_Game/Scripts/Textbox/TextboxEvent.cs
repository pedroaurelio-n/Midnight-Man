using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxEvent : MonoBehaviour
{
    public delegate void OpenTextbox(TextboxSequence sequence);
    public static event OpenTextbox onTextboxEvent;

    public void ActivateTextbox(TextboxSequence sequence)
    {
        if (onTextboxEvent != null)
        {
            onTextboxEvent(sequence);
        }
    }
}
