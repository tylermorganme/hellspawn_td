using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]
public class EnemyRunner : MonoBehaviour, IHaveHealth
{

    [SerializeField]
    Health _health;
    [SerializeField]
    float _disposeAtDepth = -50f;
    [SerializeField]
    float _disablePathfindingDepth = -1f;

    AIPath _pathfindingBrain;
    Seeker _seeker;

    public Health Health => _health;

    void Awake()
    {
        _pathfindingBrain = GetComponent<AIPath>();
        _seeker = GetComponent<Seeker>();
    }

    void  Update()
    {
        if (transform.position.y < _disposeAtDepth)
            Dispose();
        if (transform.position.y < _disablePathfindingDepth) {
            _pathfindingBrain.constrainInsideGraph = false;
            _seeker.enabled = false;
        }
    }
    public void Dispose()
    {
        Destroy(gameObject);
    }
}
