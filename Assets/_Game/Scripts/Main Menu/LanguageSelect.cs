using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LanguageSelect : MonoBehaviour
{
    [SerializeField] private Settings settings;
    [SerializeField] private Image imgFade;
    [SerializeField] private float fadeDuration;
    [SerializeField] private List<string> languageSelectTitle;
    [SerializeField] private TMP_Text txtLanguageSelectTitle;
    [SerializeField] private List<Button> btnLanguages;

    private GameObject btnSelected;

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;

        eventSystem.SetSelectedGameObject(btnLanguages[0].gameObject);
        btnSelected = eventSystem.currentSelectedGameObject;

        imgFade.DOFade(0f, 0f);
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == btnSelected)
            return;

        if (eventSystem.currentSelectedGameObject == btnLanguages[0].gameObject)
        {
            btnSelected = btnLanguages[0].gameObject;
            txtLanguageSelectTitle.text = languageSelectTitle[0];
        }

        if (eventSystem.currentSelectedGameObject == btnLanguages[1].gameObject)
        {
            btnSelected = btnLanguages[1].gameObject;
            txtLanguageSelectTitle.text = languageSelectTitle[1];
        }
    }

    public void Procceed(int value)
    {
        settings.ChangeLanguage(value);

        imgFade.DOFade(1f, fadeDuration).OnComplete(delegate {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
