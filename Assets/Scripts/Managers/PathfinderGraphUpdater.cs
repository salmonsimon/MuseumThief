using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderGraphUpdater : MonoBehaviour
{
    public void UpdatePathfinderGraphs()
    {
        Vector3 playerPosition = GameManager.instance.GetPlayer().transform.position;

        AstarPath.active.UpdateGraphs(new Bounds(playerPosition, new Vector3(1, 1, 1)));
    }
}
