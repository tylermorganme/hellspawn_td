using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct Platform {
    public Platform(int health)
    {
        Health = health;
    }
    public int Health { get; }
}

public enum Direction { Up, Down, Left, Right };

public class IslandManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformPrefab;
    [SerializeField]
    private int _startingRadius;
    [SerializeField]
    private int _platformSize = 10;
    [SerializeField]
    private AstarPath _astar;
    [SerializeField]
    private int _initialPlatformHealth = 10;

    private Dictionary<Vector2, Platform> _platforms = new Dictionary<Vector2, Platform>();
    private bool _hasNewPlatforms = false;

    private void Awake()
    {
        _platforms.Add(Vector2.zero, new Platform(_initialPlatformHealth));
        for (int x = -_startingRadius; x <= _startingRadius; x++)
        {
            for (int y = -_startingRadius; y <= _startingRadius; y++)
            {
                if ((x == 0 && y == 0) || (new Vector2(x, y).magnitude > _startingRadius))
                {
                    continue;
                }
                _platforms.Add(new Vector2(x, y), new Platform(_initialPlatformHealth));
            }
        }
        RenderPlatforms(_platforms);
        _hasNewPlatforms = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdatePathFinding();
    }

    void RenderPlatforms(Dictionary<Vector2, Platform> platforms)
    {
        foreach (var item in platforms)
        {
            // The origin is rendered separately.
            if (item.Key == Vector2.zero)
            {
                continue;
            }
            GameObject platform = Instantiate(_platformPrefab, gameObject.transform);
            platform.transform.localPosition = new Vector3(item.Key.x * +_platformSize, 0, item.Key.y * _platformSize);
            platform.transform.rotation = gameObject.transform.rotation;
        }
    }

    void AddPlatform(Vector2 coords)
    {
        _platforms.Add(coords, new Platform(_initialPlatformHealth));
        GameObject platform = Instantiate(_platformPrefab, gameObject.transform);
        platform.transform.localPosition = new Vector3(coords.x * +_platformSize, 0, coords.y * _platformSize);
        platform.transform.rotation = gameObject.transform.rotation;
        UpdatePathFinding();
        _hasNewPlatforms = true;
    }

    void GrowPlatformRandom(Direction direction)
    {
        Vector2 directionVector = Vector2.zero;
        if (direction == Direction.Up) directionVector = Vector2.up;
        if (direction == Direction.Down) directionVector = Vector2.down;
        if (direction == Direction.Left) directionVector = Vector2.left;
        if (direction == Direction.Right) directionVector = Vector2.right;

        var keys = _platforms.Keys;
        var index = Random.Range(0, keys.Count);
        KeyValuePair<Vector2, Platform> randomElement = _platforms.ElementAt(index);
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
    void GrowPlatformRandomPositiveY()
    {
        GrowPlatformRandom(Direction.Up);
    }
    [ContextMenu("GrowDown")]
    void GrowPlatformRandomNegativeY()
    {
        GrowPlatformRandom(Direction.Down);
    }
    [ContextMenu("GrowLeft")]
    void GrowPlatformRandomNegativeX()
    {
        GrowPlatformRandom(Direction.Left);
    }
    [ContextMenu("GrowRight")]
    void GrowPlatformRandomPositiveX()
    {
        GrowPlatformRandom(Direction.Right);
    }

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
