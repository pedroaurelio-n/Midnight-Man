using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public MapRooms CurrentRoomName = MapRooms.TestRoomCenter;
    public GameObject Player;
    public Enemy Enemy;

    public bool canMove = true;
    public bool isTextboxOpen = false;
    public bool isGamePaused = false;
    public bool isEnemyActive = false;
    public bool lastTextboxSequence = false;
}
