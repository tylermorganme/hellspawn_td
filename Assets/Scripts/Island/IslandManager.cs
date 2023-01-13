using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public enum Direction { Up, Down, Left, Right };

public class IslandManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformPrefab;
    [SerializeField]
    private GameObject _corePlatform;
    [SerializeField]
    private int _startingRadius;
    [SerializeField]
    private AstarPath _astar;
    [SerializeField]
    private int _platformSize = 10; // this should be a scriptable object

    private Dictionary<Vector2Int, GameObject > _platforms = new Dictionary<Vector2Int, GameObject>();
    private bool _hasNewPlatforms = false;

    private void Awake()
    {
        var gg = AstarPath.active.data.gridGraph;
        for (int z = 0; z < gg.depth; z++)
        {
            for (int x = 0; x < gg.width; x++)
            {
                Debug.Log(x+", "+z);
            }
        }
        _corePlatform.GetComponent<PlatformManager>().SetBottomScale(Vector2.zero);
        AddPlatform(Vector2Int.zero, _corePlatform);
        for (int x = -_startingRadius; x <= _startingRadius; x++)
        {
            for (int z = -_startingRadius; z <= _startingRadius; z++)
            {
                if ((x == 0 && z == 0) || (new Vector2(x, z).magnitude > _startingRadius))
                {
                    continue;
                }
                AddPlatform(new Vector2Int(x, z));
            }
        }
        _hasNewPlatforms = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdatePathFinding();
    }

    GameObject CreatePlatformGameObject(Vector2Int location, GameObject prefab)
    {
        GameObject platform = Instantiate(_platformPrefab, gameObject.transform);
        PlatformManager platformManager = platform.GetComponent<PlatformManager>();
        platform.transform.localPosition = new Vector3(location.x * +_platformSize, 0, location.y * _platformSize);
        platform.transform.rotation = gameObject.transform.rotation;
        platform.GetComponent<Health>().OnDie.AddListener(HandlePlatformDeath);
        platformManager.Coord = location;
        platformManager.SetBottomScale(location);
        return platform;
    }

    private void HandlePlatformDeath(GameObject platform)
    {
        _platforms[platform.GetComponent<PlatformManager>().Coord] = null;
        Debug.Log("I am aware a platform died");
    }

    private bool HasPlatformAtCoordinate(Vector2Int coord)
    {
        return _platforms[coord] != null;
    }

    void AddPlatform(Vector2Int coords)
    {
        GameObject platform = CreatePlatformGameObject(coords, _platformPrefab);
        _platforms.Add(coords, platform);
        UpdatePathFinding();
        _hasNewPlatforms = true;
    }

    void AddPlatform(Vector2Int coords, GameObject premadePlatform)
    {
        _platforms.Add(coords, premadePlatform);
        UpdatePathFinding();
        _hasNewPlatforms = true;
    }

    void GrowPlatformInDirection(Direction direction)
    {
        Vector2Int directionVector = Vector2Int.zero;
        if (direction == Direction.Up) directionVector = Vector2Int.up;
        if (direction == Direction.Down) directionVector = Vector2Int.down;
        // Not sure why these need to be opposite but it seems to work.
        if (direction == Direction.Left) directionVector = Vector2Int.right;
        if (direction == Direction.Right) directionVector = Vector2Int.left;

        var keys = _platforms.Keys;
        var index = Random.Range(0, keys.Count);
        KeyValuePair<Vector2Int, GameObject> randomElement = _platforms.ElementAt(index);
        Vector2Int checkPlatform = randomElement.Key;
        while (_platforms.ContainsKey(checkPlatform))
        {
            checkPlatform = checkPlatform + directionVector;
            // If it gets to the origin, keep going.
            if (checkPlatform == Vector2.zero)
            {
                checkPlatform = checkPlatform + directionVector;
            }
        }
        AddPlatform(checkPlatform);
    }

    
    [ContextMenu("GrowUp")]
    public void GrowPlatformUp() => GrowPlatformInDirection(Direction.Up);
    [ContextMenu("GrowDown")]
    public void GrowPlatformDown() => GrowPlatformInDirection(Direction.Down);
    [ContextMenu("GrowLeft")]
    public void GrowPlatformLeft() => GrowPlatformInDirection(Direction.Left);
    [ContextMenu("GrowRight")]
    public void GrowPlatformRight() => GrowPlatformInDirection(Direction.Right);

    // Update is called once per frame
    void Update()
    {
        if (_hasNewPlatforms)
        {
            UpdatePathFinding();
            _hasNewPlatforms = false;
        }

    }

    public void UpdatePathFinding()
    {
        _astar.Scan();
    }

    public Vector2Int[] NodesAtGridSpace(Vector2Int location)
    {
        Vector2Int[] baseVectors =
        {
            Vector2Int.zero,
            Vector2Int.one,
            Vector2Int.right,
            Vector2Int.up
        };
        return new Vector2Int[] {
            Vector2Int.zero + location,
            Vector2Int.one + location,
            Vector2Int.right + location,
            Vector2Int.up + location
        };
    }
}
