using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TextboxManager : MonoBehaviour
{
    public delegate void TextboxDialogEnded();
    public static event TextboxDialogEnded onTextboxDialogEnded;

    [SerializeField] private Image textboxBackground;

    [SerializeField] private Image characterFrameUI;
    [SerializeField] private Image characterPortraitUI;
    [SerializeField] private TMP_Text characterNameUI;
    [SerializeField] private TMP_Text portraitTextUI;
    [SerializeField] private TMP_Text fullTextUI;

    [SerializeField] private Button yesActionButton;
    [SerializeField] private Button noActionButton;

    [SerializeField] private float openScaleTime;
    [SerializeField] private float openFadeTime;

    [SerializeField] private float closeScaleTime;
    [SerializeField] private float closeFadeTime;

    [SerializeField] private float letterDelay;

    private bool isAnimating;
    private bool isTypingLetters;
    public bool hasAcceptedAction;

    private TextboxSequence _sequence;
    private string _currentText;

    private int _sequenceCurrentIndex;
    private int _textCurrentIndex;

    private bool hasMoreText;
    private bool hasMoreSequence;

    private PlayerInputControls playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputControls();
    }

    private void Start()
    {
        playerInput.Gameplay.Interaction.performed += ProgressText;

        StartHideAll();
    }

    public void ReadInformation(TextboxSequence sequence)
    {
        _sequence = sequence;

        _sequenceCurrentIndex = 0;
        _textCurrentIndex = 0;

        UpdateBoxInfo();
        ShowTextbox();
    }

    public void UpdateBoxInfo()
    {
        _currentText = _sequence.TextSequence[_sequenceCurrentIndex].TextList[_textCurrentIndex];

        if (_sequence.TextSequence[_sequenceCurrentIndex].CharacterPortrait != null)
        {
            characterFrameUI.gameObject.SetActive(true);
            characterNameUI.gameObject.SetActive(true);
            characterPortraitUI.gameObject.SetActive(true);

            portraitTextUI.gameObject.SetActive(true);

            characterNameUI.text = _sequence.TextSequence[_sequenceCurrentIndex].CharacterName;
            characterPortraitUI.sprite = _sequence.TextSequence[_sequenceCurrentIndex].CharacterPortrait;
        }

        else
        {
            characterFrameUI.gameObject.SetActive(false);
            characterNameUI.gameObject.SetActive(false);
            characterPortraitUI.gameObject.SetActive(false);

            fullTextUI.gameObject.SetActive(true);
        }
    }

    public void ProgressText(InputAction.CallbackContext ctx)
    {
        if (isAnimating || !GameManager.Instance.isTextboxOpen)
            return;

        portraitTextUI.text = "";
        fullTextUI.text = "";

        AudioManager.Instance.PlayAudio(1);

        hasMoreText = _textCurrentIndex < _sequence.TextSequence[_sequenceCurrentIndex].TextList.Count - 1;
        hasMoreSequence = _sequenceCurrentIndex < _sequence.TextSequence.Count - 1;

        if (isTypingLetters)
        {
            StopAllCoroutines();
            portraitTextUI.text = _currentText;
            fullTextUI.text = _currentText;
            isTypingLetters = false;

            if (_sequence.TextSequence[_sequenceCurrentIndex].hasAction && !yesActionButton.gameObject.activeSelf)
            {
                yesActionButton.gameObject.SetActive(true);
                noActionButton.gameObject.SetActive(true);

                EventSystem.current.SetSelectedGameObject(yesActionButton.gameObject);
                EventSystem.current.sendNavigationEvents = true;

                StartCoroutine(AddListeners());
            }
            return;
        }

        if (hasMoreText)
        {
            _textCurrentIndex++;
            UpdateBoxInfo();
            StartCoroutine(TypeText());
            return;
        }

        _textCurrentIndex = 0;

        if (hasMoreSequence && !_sequence.TextSequence[_sequenceCurrentIndex].hasAction)
        {
            _sequenceCurrentIndex++;
            StartCoroutine(TextboxPersonChange());
            return;
        }

        if (!hasMoreText && !hasMoreSequence && !yesActionButton.gameObject.activeSelf)
        {
            HideTextbox(false);
            return;
        }
    }

    private IEnumerator AddListeners()
    {
        hasAcceptedAction = false;

        yield return new WaitForSecondsRealtime(0.1f);

        yesActionButton.onClick.RemoveAllListeners();
        yesActionButton.onClick.AddListener(delegate {
            hasAcceptedAction = true;
            GameObject temp = Instantiate(_sequence.TextSequence[_sequenceCurrentIndex].ActivateEffect);
            Destroy(temp, 1f);

            yesActionButton.gameObject.SetActive(false);
            noActionButton.gameObject.SetActive(false);

            if (hasMoreSequence)
            {
                _sequenceCurrentIndex++;
                StartCoroutine(TextboxPersonChange());
            }
        });

        noActionButton.onClick.RemoveAllListeners();
        noActionButton.onClick.AddListener(delegate {
            HideTextbox(false);
        });
    }

    private IEnumerator TypeText()
    {
        isTypingLetters = true;

        foreach (char letter in _currentText.ToCharArray())
        {
            portraitTextUI.text += letter;
            fullTextUI.text += letter;
            AudioManager.Instance.PlayAudio(5);

            yield return new WaitForSecondsRealtime(letterDelay);
        }

        isTypingLetters = false;

        if (_sequence.TextSequence[_sequenceCurrentIndex].hasAction && !yesActionButton.gameObject.activeSelf)
        {
            yesActionButton.gameObject.SetActive(true);
            noActionButton.gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(yesActionButton.gameObject);
            EventSystem.current.sendNavigationEvents = true;

            StartCoroutine(AddListeners());
        }
    }

    private IEnumerator TextboxPersonChange()
    {
        HideTextbox(true);
        yield return new WaitForSecondsRealtime(closeScaleTime);
        UpdateBoxInfo();
        yield return new WaitForSecondsRealtime(closeScaleTime * 0.25f);
        ShowTextbox();
    }

    private void ShowTextbox()
    {
        portraitTextUI.text = "";
        fullTextUI.text = "";

        Tween fadeIn = textboxBackground.transform.DOScale(1f, openScaleTime).OnComplete(delegate
        {
            characterPortraitUI.DOFade(1f, openFadeTime).SetUpdate(true);
            characterNameUI.DOFade(1f, openFadeTime).SetUpdate(true).OnComplete(delegate
            { 
                isAnimating = false;
                StartCoroutine(TypeText());
            }).SetUpdate(true);
        }).SetUpdate(true);

        fadeIn.OnStart(delegate 
        {
            GameManager.Instance.isTextboxOpen = true;
            GameManager.Instance.canMove = false;
            Time.timeScale = 0;
            isAnimating = true;
            EventSystem.current.sendNavigationEvents = false;
        });
    }

    private void HideTextbox(bool continueBool)
    {
        portraitTextUI.gameObject.SetActive(false);
        fullTextUI.gameObject.SetActive(false);

        Tween fadeOut = textboxBackground.transform.DOScale(0f, closeScaleTime).OnComplete(delegate
        {
            if (!continueBool)
            {
                isAnimating = false;
                GameManager.Instance.isTextboxOpen = false;
                EventSystem.current.sendNavigationEvents = true;

                if (!GameManager.Instance.isGamePaused)
                {
                    GameManager.Instance.canMove = true;
                    Time.timeScale = 1;
                }

                if (onTextboxDialogEnded != null)
                {
                    onTextboxDialogEnded();
                }
            }
        }).SetUpdate(true);

        fadeOut.OnStart(delegate 
        {
            yesActionButton.gameObject.SetActive(false);
            noActionButton.gameObject.SetActive(false);

            isAnimating = true; 

            characterPortraitUI.DOFade(0f, closeFadeTime).SetUpdate(true);
            characterNameUI.DOFade(0f, closeFadeTime).SetUpdate(true);
        }).SetUpdate(true);
    }

    private void StartHideAll()
    {
        textboxBackground.transform.DOScale(0f, 0);

        characterPortraitUI.DOFade(0f, 0);
        characterNameUI.DOFade(0f, 0);
        portraitTextUI.gameObject.SetActive(false);
        fullTextUI.gameObject.SetActive(false);
    }

    #region Enable/Disable
    private void OnEnable()
    {
        playerInput.Enable();
        TextboxEvent.onTextboxEvent += ReadInformation;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        TextboxEvent.onTextboxEvent -= ReadInformation;
    }
#endregion
}
