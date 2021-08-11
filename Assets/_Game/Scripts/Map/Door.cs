using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public delegate void ChangeRoom(Vector3 doorPosition, Room currentRoom, MapRooms currentRoomName, MapRooms nextRoomName);
    public static event ChangeRoom onRoomChange;

    public MapRooms currentRoomName;
    public MapRooms nextRoomName;
    public Transform teleportPoint;
    public bool canTeleportPlayer = false;

    public Room currentRoom;
    public Room nextRoom;

    [SerializeField] private Image imageTransition;

    private IEnumerator TeleportPlayer(Player player)
    {
        GameManager.Instance.CurrentRoomName = nextRoomName;
        GameManager.Instance.canMove = false;
        Time.timeScale = 0;

        imageTransition.DOFade(1f, 1f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.1f);

        player.transform.position = teleportPoint.position;
        yield return new WaitForSecondsRealtime(0.1f);

        GameManager.Instance.canMove = true;
        Time.timeScale = 1;
        imageTransition.DOFade(0f, 1f).SetUpdate(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player) && canTeleportPlayer)
        {
            StartCoroutine(TeleportPlayer(player));
            AudioManager.Instance.PlayAudio(4);

            if (onRoomChange != null)
            {
                onRoomChange(transform.position, currentRoom, currentRoomName, nextRoomName);
                print("dispara evento");
            }
        }

        if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.RemoveDestination(nextRoomName);
            enemy.transform.position = teleportPoint.position;
        }
    }
}
