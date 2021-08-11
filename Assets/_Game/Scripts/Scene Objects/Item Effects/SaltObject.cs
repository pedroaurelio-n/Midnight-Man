using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SaltObject : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Image fadeImage;
    [SerializeField] private List<TextboxSequence> successSequence;

    private void Action(Item item)
    {
        pauseMenu.InstantResumeGame();
        fadeImage.DOFade(1f, 1f).OnComplete(delegate
        {
            Time.timeScale = 0;
            GameManager.Instance.canMove = false;
            GameManager.Instance.canMove = false;
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(successSequence[DataHolder.Instance.settings.Language_Id]);
            GameManager.Instance.lastTextboxSequence = true;
        });
    }

    private void EndGame()
    {
        if (GameManager.Instance.lastTextboxSequence)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnEnable()
    {
        SaltEvent.onSaltAction += Action;
        TextboxManager.onTextboxDialogEnded += EndGame;
    }

    private void OnDisable()
    {
        SaltEvent.onSaltAction -= Action;
        TextboxManager.onTextboxDialogEnded -= EndGame;
    }
}
