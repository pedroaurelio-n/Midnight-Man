using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField] private GameObject pnlCredits;
    [SerializeField] private Image fadeTransition;
    [SerializeField] private Image imgLogo;
    [SerializeField] private float YValue;
    [SerializeField] private float scrollTime;
    [SerializeField] private Ease ease;

    private void Start()
    {
        fadeTransition.DOFade(1f, 0f);
        imgLogo.DOFade(0f, 0f);

        StartCoroutine(CreditsRoutine());
    }

    private IEnumerator CreditsRoutine()
    {
        fadeTransition.DOFade(0f, 1f);
        yield return new WaitForSeconds(2f);

        imgLogo.DOFade(1f, 1f);
        yield return new WaitForSeconds(2f);

        pnlCredits.transform.DOMoveY(YValue, scrollTime).SetEase(ease);
        yield return new WaitForSeconds(scrollTime);

        yield return new WaitForSeconds(4f);

        fadeTransition.DOFade(1f, 1f);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(1);
    }
}
