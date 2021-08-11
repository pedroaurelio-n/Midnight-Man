using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MatchBoxObject : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private List<TextboxSequence> successSequence;
    [SerializeField] private List<GameObject> gameobjectList;

    private void Action(Item item)
    {
        StartCoroutine(ActionRoutine(item));
    }

    private IEnumerator ActionRoutine(Item item)
    {
        pauseMenu.InstantResumeGame();
        GameManager.Instance.canMove = false;
        Time.timeScale = 0;

        fadeImage.DOFade(1f, 1f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.1f);

        player.hasCandle = true;
        playerLight.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);

        GameManager.Instance.canMove = true;
        Time.timeScale = 1;
        fadeImage.DOFade(0f, 1f).SetUpdate(true).OnComplete(delegate { 
            gameObject.GetComponent<TextboxEvent>().ActivateTextbox(successSequence[DataHolder.Instance.settings.Language_Id]);

            foreach (GameObject obj in gameobjectList)
            {
                obj.SetActive(!obj.activeSelf);
            }

            inventoryUI.RemoveItem(item);
        });
    }

    private void OnEnable()
    {
        MatchBoxEvent.onMatchBoxAction += Action;
    }

    private void OnDisable()
    {
        MatchBoxEvent.onMatchBoxAction -= Action;
    }
}
