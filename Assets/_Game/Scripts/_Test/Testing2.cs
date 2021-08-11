using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing2 : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Transform gridParent;

    [SerializeField] private LayerMask mask;
    private Pathfinding2 pathfinding;

    public List<Vector3> path;

    private Vector3 gridOriginPosition;

    private void Start()
    {
        pathfinding = new Pathfinding2(gridParent, width, height, cellSize, mask, transform.position);

        gridOriginPosition = pathfinding.GetGrid().GetOriginPosition();
    }

    private void Pathfinding()
    {
        if (!enemy.canMove)
            return;

        Vector3 targetPosition;
        bool isInSameRoom;

        if (enemy.movePoints.Count > 0)
        {
            targetPosition = enemy.movePoints[0].position;
            isInSameRoom = false;
        }

        else
        {
            targetPosition = enemy.player.position;
            isInSameRoom = true;
        }

        enemy.SetTargetPosition(targetPosition, isInSameRoom);

        path = pathfinding.FindPath(enemy.gameObject.transform.position, targetPosition);

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(path[i] * cellSize + Vector3.one * cellSize * 0.5f, path[i + 1] * cellSize + Vector3.one * cellSize * 0.5f, Color.red, 0.1f);
            }
        }
    }

    private void FixedUpdate()
    {
        Pathfinding();
    }
}
