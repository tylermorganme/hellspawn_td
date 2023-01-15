using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

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
    private List<Vector2Int> _walkableAreas;
    private List<Vector2Int> _blockedAreas;


    private void Awake()
    {
        _astar.data.gridGraph.Scan();
        _walkableAreas = new List<Vector2Int> { };
        _blockedAreas = new List<Vector2Int> { };
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
    }

    GameObject CreatePlatformGameObject(Vector2Int location, GameObject prefab)
    {
        GameObject platform = Instantiate(_platformPrefab, gameObject.transform);
        PlatformManager platformManager = platform.GetComponent<PlatformManager>();
        platform.transform.localPosition = new Vector3(location.x * +_platformSize, 0, location.y * _platformSize);
        platform.transform.rotation = gameObject.transform.rotation;
        platformManager.Coord = location;
        platformManager.SetBottomScale(location);
        return platform;
    }

    public void HandleStrayPlatformCollison(IEnumerable<Transform> transforms)
    {
        foreach (Transform trans in transforms)
        {
            // Convert to local space
            Vector3 transformedPosition = transform.InverseTransformPoint(trans.position);

            // Round to grid spacing
            int gridX = (int)Mathf.Round(transformedPosition.x / 10);
            int gridZ = (int)Mathf.Round(transformedPosition.z / 10);
            Debug.Log(gridX + ", " + gridZ);
            AddPlatform(new Vector2Int(gridX, gridZ));
        }
        //Debug.Log(transforms.Count());
    }

    private bool HasPlatformAtCoordinate(Vector2Int coord)
    {
        return _platforms.GetValueOrDefault(coord) != null;
    }

    void AddPlatform(Vector2Int coord)
    {
        if (!HasPlatformAtCoordinate(coord))
        {
            GameObject platform = CreatePlatformGameObject(coord, _platformPrefab);
            _platforms.Add(coord, platform);
            _walkableAreas.Add(coord);
        }
    }

    void AddPlatform(Vector2Int coords, GameObject premadePlatform)
    {
        _platforms.Add(coords, premadePlatform);
        _walkableAreas.Add(coords);
    }

    void LateUpdate()
    {

        UpdatePathFinding();
    }

    public void UpdatePathFinding()
    {
        SetPlatformsWalkability(_walkableAreas, true);
        SetPlatformsWalkability(_blockedAreas, false);
        SetPlatformsWalkability(_blockedAreas, false);
        _walkableAreas = new List<Vector2Int> { };
        _blockedAreas = new List<Vector2Int> { };
    }

    public Vector2Int[] GetGridIndicesAtPlatform(Vector2Int location)
    {

        Vector2Int modifiedLocation = Vector2Int.one * 49 + location * 2;

        return new Vector2Int[] {
            Vector2Int.zero + modifiedLocation,
            Vector2Int.one + modifiedLocation,
            Vector2Int.right + modifiedLocation,
            Vector2Int.up + modifiedLocation
        };
    }

    public void HandleWallBuilt(Vector2Int location)
    {
        Debug.Log("XXX");
        _blockedAreas.Add(location);
    }

    public void HandleWallRemoved(Vector2Int location)
    {
        _walkableAreas.Add(location);
    }

    public void HandlePlatformDeath(Vector2Int location)
    {
        _blockedAreas.Add(location);
        _platforms[location] = null;
        Debug.Log("I am aware a platform died");
    }

    public void SetPlatformsWalkability(List<Vector2Int> locations, bool isWalkable)
    {
        List<Vector2Int> locationVectors= new List<Vector2Int> { };
        
        foreach (Vector2Int location in locations)
        {
            locationVectors.AddRange(GetGridIndicesAtPlatform(location));
        }

        //= GetGridIndicesAtPlatform(location);
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx =>
        {
            var gg = AstarPath.active.data.gridGraph;
            foreach (Vector2Int vector in locationVectors)
            {
                var node = gg.GetNode(vector.x, vector.y);
                node.Walkable = isWalkable;
                gg.CalculateConnectionsForCellAndNeighbours(vector.x, vector.y);
            }

            // https://arongranberg.com/astar/docs/graphupdates.html#smaller-updates
            // Recalculate all grid connections
            // This is required because we have updated the walkability of some nodes
            //gg.GetNodes(node => gg.CalculateConnections((GridNodeBase)node));
        }));
    }
}
