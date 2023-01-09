using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private int _platformSize = 10;
    [SerializeField]
    private AstarPath _astar;
    [SerializeField]
    private int _initialPlatformHealth = 10;

    private Dictionary<Vector2, GameObject > _platforms = new Dictionary<Vector2, GameObject>();
    private bool _hasNewPlatforms = false;

    private void Awake()
    {
        AddPlatform(Vector2.zero, _corePlatform);
        for (int x = -_startingRadius; x <= _startingRadius; x++)
        {
            for (int y = -_startingRadius; y <= _startingRadius; y++)
            {
                if ((x == 0 && y == 0) || (new Vector2(x, y).magnitude > _startingRadius))
                {
                    continue;
                }
                AddPlatform(new Vector2(x, y));
            }
        }
        _hasNewPlatforms = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdatePathFinding();
    }


    GameObject CreatePlatformGameObject(float x, float y, GameObject prefab)
    {
        GameObject platform = Instantiate(_platformPrefab, gameObject.transform);
        platform.transform.localPosition = new Vector3((int)x * +_platformSize, 0, (int)y * _platformSize);
        platform.transform.rotation = gameObject.transform.rotation;
        platform.GetComponent<PlatformManager>().X = x;
        platform.GetComponent<PlatformManager>().Y = y;
        return platform;
    }

    void AddPlatform(Vector2 coords)
    {
        GameObject platform = CreatePlatformGameObject(coords.x, coords.y, _platformPrefab);
        _platforms.Add(coords, platform);
        UpdatePathFinding();
        _hasNewPlatforms = true;
    }

    void AddPlatform(Vector2 coords, GameObject premadePlatform)
    {
        _platforms.Add(coords, premadePlatform);
        UpdatePathFinding();
        _hasNewPlatforms = true;
    }

    void GrowPlatformInDirection(Direction direction)
    {
        Vector2 directionVector = Vector2.zero;
        if (direction == Direction.Up) directionVector = Vector2.up;
        if (direction == Direction.Down) directionVector = Vector2.down;
        // Not sure why these need to be opposite but it seems to work.
        if (direction == Direction.Left) directionVector = Vector2.right;
        if (direction == Direction.Right) directionVector = Vector2.left;

        var keys = _platforms.Keys;
        var index = Random.Range(0, keys.Count);
        KeyValuePair<Vector2, GameObject> randomElement = _platforms.ElementAt(index);
        Vector2 checkPlatform = randomElement.Key;
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

    void UpdatePathFinding()
    {
        _astar.Scan();
    }
}
