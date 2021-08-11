using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;
    public List<Vector3> path;

    private void Start()
    {
        pathfinding = new Pathfinding(10, 10, 2f, transform.position);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            print($"{x},{y}");
            path = pathfinding.FindPath(transform.position, mouseWorldPosition);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.red, 100f);
                }
            }
        }
    }
}
