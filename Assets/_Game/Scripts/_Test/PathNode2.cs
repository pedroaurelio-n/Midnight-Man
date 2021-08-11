using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode2
{
    public bool isWalkable;
    private int value;

    private Grid2<PathNode2> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode2 cameFromNode;

    public PathNode2(Grid2<PathNode2> grid, int x, int y, bool isWalkable)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        grid.TriggerGridObjectChanged(x, y);
        Debug.Log(value);
    }

    public override string ToString()
    {
        if (!isWalkable)
            return "X";

        else
            return "";
    }
}
