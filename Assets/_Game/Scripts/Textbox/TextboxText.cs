using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextboxText
{
    public string CharacterName;
    public Sprite CharacterPortrait;
    public bool hasAction;
    public GameObject ActivateEffect;

    public List<string> TextList = new List<string>();
}
