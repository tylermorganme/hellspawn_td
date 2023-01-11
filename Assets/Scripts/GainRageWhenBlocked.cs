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
    private static GraphNode _graphCenter;
    private static int _lastFrameUpdate = -1;

    // This is a vector that

    private void Awake()
    {
        float nodeSize;
        _seeker = GetComponent<Seeker>();
        _seeker.pathCallback += HandlePathChange;
        _rage = GetComponent<Rage>();
        _astar = FindObjectOfType<AstarPath>();
        nodeSize = _astar.data.gridGraph.nodeSize;
    }

    private void Update()
    {
        if (Time.frameCount > _lastFrameUpdate)
        {
            _graphCenter = AstarPath.active.GetNearest(FindObjectOfType<GameManager>().Island.transform.position, NNConstraint.Default).node;
        }
    }

    private void HandlePathChange(Path p)
    {
        //Inspired by https://forum.arongranberg.com/t/can-i-know-whether-a-static-destination-is-ever-reachable/7788
        Debug.Log("Center: " + _graphCenter.position);
        Debug.Log("Target: " + p.path?.LastOrDefault()?.position);
        if (p.path?.LastOrDefault()?.position == _graphCenter.position)
        {
            Debug.Log(p.path.LastOrDefault().position);
            Debug.Log("Valid Path");
            return;
        }
        Debug.Log("No Valid Path");
        _rage.GainRage();

    }
    private void OnDisable()
    {
        _seeker.pathCallback -= HandlePathChange;
    }
}
