using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sequence", menuName = "Text Sequence")]
public class TextboxSequence : ScriptableObject
{
    public List<TextboxText> TextSequence;
}
