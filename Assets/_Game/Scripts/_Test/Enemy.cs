using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image gameoverImage;
    [SerializeField] private AudioClip gameoverClip;

    [SerializeField] private float maxTime;
    [SerializeField] private float mass;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    public bool canMove;
    [SerializeField] private Transform restPosition;

    public List<Transform> movePoints = new List<Transform>();
    [HideInInspector] public Transform player;

    private MapRooms currentRoomName;
    private List<Door> doorsInRoom = new List<Door>();

    private Rigidbody2D rb;

    private float time;
    private bool isInSameRoom;

    private List<Vector3> pathVectorList;
    private int currentPathIndex;

    private Vector3 targetPosition;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Vector2 steeringVelocity;
    private Vector2 moveDirection;

    private void Start()
    {
        time = maxTime;

        rb = GetComponent<Rigidbody2D>();

        gameoverImage.DOFade(0f, 0f);

        currentRoomName = GameManager.Instance.CurrentRoomName;

        player = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        HandleMovement();

        rb.velocity = velocity;
    }

    private void HandleMovement()
    {
        if (!canMove)
            return;

        if (!isInSameRoom)
        {
            time -= Time.deltaTime;
        }

        if (pathVectorList != null)
        {
            targetPosition = pathVectorList[currentPathIndex];

            if (Vector3.Distance(transform.position, targetPosition) > minDistance)
            {
                moveDirection = (targetPosition - transform.position).normalized;
            }

            else
            {
                currentPathIndex++;

                if (currentPathIndex >= pathVectorList.Count)
                {
                    pathVectorList = null;
                }
            }
        }

        desiredVelocity = moveDirection * speed;

        steeringVelocity = desiredVelocity - velocity;
        steeringVelocity = steeringVelocity / mass;

        velocity += steeringVelocity;

        if (time <= 0)
        {
            DeactivateEnemy();
        }
    }

    public void DeactivateEnemy()
    {
        transform.position = restPosition.position;
        canMove = false;
        time = maxTime;
        rb.velocity = Vector2.zero;
        velocity = Vector2.zero;
        GameManager.Instance.isEnemyActive = false;
    }

    public void ActivateEnemy(Transform newPosition)
    {
        AudioManager.Instance.PlayAudio(6);
        transform.position = newPosition.position;
        currentRoomName = GameManager.Instance.CurrentRoomName;
        movePoints.Clear();
        canMove = true;
        GameManager.Instance.isEnemyActive = true;

    }

    public void ActivateEnemyTurbo(Transform newPosition)
    {
        ActivateEnemy(newPosition);
        time = maxTime * 5;
        speed = speed * 1.35f;
        GameManager.Instance.isEnemyActive = true;
    }

    private void AddDestination(Vector3 doorPosition, Room currentRoom, MapRooms currentRoomName, MapRooms nextRoomName)
    {
        //print("inicia método");
        if (nextRoomName == this.currentRoomName)
        {
            //print("se mesma sala, limpa");
            movePoints.Clear();
            return;
        }

        //print("pega portas da sala");
        doorsInRoom = currentRoom.DoorList;
        //print("portas da sala = " + doorsInRoom.Count);

        foreach (Door door in doorsInRoom)
        {
            //print("para cada porta na sala");
            if (door.nextRoomName == nextRoomName)
            {
                //print("se porta certa, seguir");
                movePoints.Add(door.gameObject.transform);
                break;
            }
        }
    }

    public void RemoveDestination(MapRooms nextRoomName)
    {
        if (nextRoomName == currentRoomName)
        {
            movePoints.Clear();
            return;
        }

        currentRoomName = nextRoomName;

        movePoints.RemoveAt(0);
    }

    public void SetTargetPosition(Vector3 targetPosition, bool sameRoom)
    {
        currentPathIndex = 0;
        isInSameRoom = sameRoom;
        pathVectorList = Pathfinding2.Instance.FindPath(transform.position, targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    private IEnumerator GameOver()
    {
        MusicManager.Instance.ChangeSong(gameoverClip);

        GameManager.Instance.canMove = false;
        Time.timeScale = 0;

        gameoverImage.DOFade(1f, 1f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(4f);
        
        gameoverImage.DOColor(Color.black, 1f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(GameOver());
        }
    }

    private void OnEnable()
    {
        Door.onRoomChange += AddDestination;
    }

    private void OnDisable()
    {
        Door.onRoomChange += AddDestination;
    }
}
