using Pathfinding;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rage))]
public class GainRageWhenBlocked : MonoBehaviour
{
    private Seeker _seeker;
    private Rage _rage;
    private AstarPath _astar;
    private GraphNode _graphCenter; // This is a vector that

    private void Awake()
    {
        float nodeSize;
        _seeker = GetComponent<Seeker>();
        _seeker.pathCallback += HandlePathChange;
        _rage = GetComponent<Rage>();
        _astar = FindObjectOfType<AstarPath>();
        nodeSize = _astar.data.gridGraph.nodeSize;

        // Hard  coding this to zero won't work if the island is moved. Will need to grab a reference to to the island
        _graphCenter = AstarPath.active.GetNearest(FindObjectOfType<GameManager>().Island.transform.position, NNConstraint.Default).node;
    }

    private void HandlePathChange(Path p)
    {
        //Inspired  by https://forum.arongranberg.com/t/can-i-know-whether-a-static-destination-is-ever-reachable/7788
        //GraphNode nearestNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
        //if (nearestNode == null)
        //    return;
        if (p.path?.LastOrDefault()?.position == _graphCenter.position)
        //if (PathUtilities.IsPathPossible(_graphCenter, nearestNode))
        {
            Debug.Log(p.path.LastOrDefault().position);
            Debug.Log("Valid Path");
            return;
        }
        _rage.GainRage();

    }
    private void OnDisable()
    {
        _seeker.pathCallback -= HandlePathChange;
    }
}
