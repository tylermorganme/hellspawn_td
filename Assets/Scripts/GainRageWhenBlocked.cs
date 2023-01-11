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
    private Int3 _graphCenter; // This is a vector that

    private void Awake()
    {
        float nodeSize;
        _seeker = GetComponent<Seeker>();
        _seeker.pathCallback += GainRage;
        _rage = GetComponent<Rage>();
        _astar = FindObjectOfType<AstarPath>();
        nodeSize = _astar.data.gridGraph.nodeSize;
        _graphCenter = new Int3(1, 0, 1) * nodeSize * 1000 / 2;
    }

    private void GainRage(Path p)
    {
        // When the seeker things it's headed to the center it has a target of
        //Debug.Log("Center: " + _graphCenter);
        //Debug.Log("Target: " + p.path.Last().position);
        //Debug.Log(p.path.Last().position.Equals(_graphCenter));
        if (p.path?.LastOrDefault()?.position != _graphCenter)
        {
            _rage.GainRage();
            //Debug.Log("Should Gain Rage! RAAARRRR!");
        }
    }
    private void OnDisable()
    {
        _seeker.pathCallback -= GainRage;
    }
}
