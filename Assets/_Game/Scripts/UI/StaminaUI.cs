using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StaminaUI : MonoBehaviour
{
    public delegate void PlayerStaminaEmpty();
    public static event PlayerStaminaEmpty onZeroStamina;

    [SerializeField] private float completionFadeDelay;
    [SerializeField] private float fadeDuration;

    [SerializeField] private CanvasGroup panelGroup;
    [SerializeField] private Image staminaImage;

    private bool isActive = false;
    private bool isFading;
    private bool isStaminaEmpty;

    private float delay = 0;

    private float _staminaValue;

    private void Update()
    {
        if (_staminaValue != 10)
            return;

        if (delay <= completionFadeDelay)
        {
            delay += Time.deltaTime;
        }

        else if (delay > completionFadeDelay && isActive && !isFading)
        {
            isActive = false;
            isFading = true;
            panelGroup.DOFade(0, fadeDuration).OnComplete(delegate { isFading = false; });
        }
    }

    private void UpdateStamina(float maxStamina, float newStamina, bool isSpendingStamina)
    {
        if (isSpendingStamina)
        {
            delay = 0;
        }

        if (!isActive && isSpendingStamina)
        {
            if (!isFading)
            {
                isActive = true;
                panelGroup.DOFade(1, fadeDuration);
                StopAllCoroutines();
            }

            else
            {
                isActive = true;
                panelGroup.DOComplete();
                panelGroup.alpha = 1;
            }
        }

        _staminaValue = Mathf.Clamp(newStamina, 0, maxStamina);

        staminaImage.fillAmount = _staminaValue / maxStamina;

        if (_staminaValue <= 0)
        {
            isStaminaEmpty = true;
            staminaImage.color = Color.red;

            if (onZeroStamina != null)
                onZeroStamina();
        }

        if (isStaminaEmpty && _staminaValue >= maxStamina)
        {
            staminaImage.color = Color.green;

            if (onZeroStamina != null)
                onZeroStamina();

            isStaminaEmpty = false;
        }
    }

    private void OnEnable()
    {
        PlayerMovement.onStaminaChange += UpdateStamina;
    }

    private void OnDisable()
    {
        PlayerMovement.onStaminaChange -= UpdateStamina;
    }
}
