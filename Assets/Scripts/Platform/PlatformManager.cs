using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject _wallPrefab;
    [SerializeField]
    GameObject _towerPrefab;
    [SerializeField]
    GameObject _platformBottom;
    [SerializeField]
    private float _timeToDestroy = 20;
    [SerializeField]
    private int _platformSize = 10; // this should be a scriptable object

    GameObject _wall;
    GameObject _tower;
    Vector2Int _coord;
    IslandManager _islandManager;
    bool _shouldUpdatePathfinding = false;

    public bool HasWall => _wall != null;
    public bool HasTower => _tower != null;
    public bool HasNoBuildings => _tower == null & _wall == null;
    public Vector2Int Coord
    {
        get
        {
            return _coord;
        }
        set
        {
            _coord = value;
        }
    }

    private void Awake()
    {
        _islandManager = FindObjectOfType<GameManager>().IslandManager;
    }

    private void BuildWall()
    {
        if (_wall == null)
        {
            _islandManager.HandleWallBuilt(_coord);
            _wall = Instantiate(_wallPrefab, transform.position, transform.rotation, gameObject.transform);
        }
    }

    private void BuildTower()
    {
        if (_tower == null)
        {
            _tower = Instantiate(_towerPrefab, transform.position, transform.rotation, gameObject.transform);
        }
    }

    private void RemoveWall()
    {
        _islandManager.HandleWallRemoved(_coord);
        // This should be replace with a Destroy method on the wall.
        Destroy(_wall);
    }

    private void DestroyTower()
    {
        // This should be replace with a Destroy method on the tower.
        Destroy(_tower);
    }

    public void TryApplyCurrency(Currency c)
    {
        switch (c)
        {
            case Currency.Creation:
                ApplyCreationCurrency();
                break;
            case Currency.Destruction:
                ApplyDestructionCurrency();
                break;
            default:
                Debug.LogWarning("Unknown currency type used");
                break;
        }

    }

    [ContextMenu("ApplyCreationCurrency")]
    private void ApplyCreationCurrency()
    {
        if (HasTower)
        {
            return;
        }
        else if (HasWall)
        {
            // TODO: Reduce currency
            BuildTower();
        }
        else
        {
            // TODO: Reduce currency
            BuildWall();
        }
    }

    [ContextMenu("ApplyDestructionCurrency")]
    private void ApplyDestructionCurrency()
    {
        if (HasTower)
        {
            // TODO: Reduce currency
            DestroyTower();
            return;
        } else if (HasWall)
        {
            // TODO: Reduce currency
            RemoveWall();
            return;
        }
    }

    public void HandleDeath()
    {
        _islandManager.HandlePlatformDeath(_coord);
        Dispose();
    }

    public void ActivateGravity()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }
    private void Dispose()
    {
        transform.localScale *= 0.95f;
        ActivateGravity();
        StartCoroutine(DestroyAfterTime(_timeToDestroy));
    }

    private IEnumerator DestroyAfterTime(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }

    public void SetBottomScale(Vector2 location)
    {
        float y = Mathf.Max((100 - location.magnitude * _platformSize * 1.5f) * Random.Range(0.5f, 1), 0);
        _platformBottom.transform.localScale = new Vector3(1, y, 1);
    }
}
