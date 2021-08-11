using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIElementsLanguage : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] [Multiline(3)] private List<string> textLanguages;

    private void Start()
    {
        textComponent.text = textLanguages[CheckLanguage()];
    }

    private void OnEnable()
    {
        textComponent.text = textLanguages[CheckLanguage()];
    }

    private int CheckLanguage()
    {
        return DataHolder.Instance.settings.Language_Id;
    }
}
