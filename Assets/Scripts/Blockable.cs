using Pathfinding;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rage))]
public class Blockable : MonoBehaviour
{


    [SerializeField]
    bool _shouldLog;

    static GraphNode _graphCenter;
    static int _lastFrameUpdate = -1;
    Seeker _seeker;
    bool _isBlocked = false;

    public UnityEvent OnBlockStart;
    public UnityEvent OnBlockEnd;
    public UnityEvent OnBlocked;

    // This is a vector that

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _seeker.pathCallback += HandlePathChange;
    }

    // Right now this works because everything has a single target, the center of the island.
    // This could be changed to use the GameObject IAStarAI to udpate the target location every frame. 
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
        if(_shouldLog)
            Debug.Log("Center: " + _graphCenter.position);
        if (_shouldLog)
            Debug.Log("Target: " + p.path?.LastOrDefault()?.position);

        bool notBlocked = p.path?.LastOrDefault()?.position == _graphCenter.position;
        // Not Blocked
        if (notBlocked)
        {
            if (_shouldLog)
                Debug.Log("Not Blocked");
            if (_isBlocked)
                OnBlockEnd?.Invoke();
            _isBlocked = false;
        } else
        {
        // Is Blocked
            OnBlocked?.Invoke();
            if (!_isBlocked)
                OnBlockStart?.Invoke();
            _isBlocked = true;
        }

    }
    private void OnDisable()
    {
        _seeker.pathCallback -= HandlePathChange;
    }
}
