using UnityEngine;


public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject _wallPrefab;
    [SerializeField]
    GameObject _towerPrefab;

    private GameObject _wall;
    private GameObject _tower;
    public bool HasWall => _wall != null;
    public bool HasTower => _tower != null;
    private AstarPath _astar;
    private bool _shouldUpdatePathfinding = false;

    private void Awake()
    {
        _astar = FindObjectOfType<AstarPath>();
    }

    private void Update()
    {
        if (_shouldUpdatePathfinding)
        {
            UpdatePathfinding();
        }
    }

    private void BuildWall()
    {
        if (_wall == null)
        {
            _wall = Instantiate(_wallPrefab, transform.position, transform.rotation, gameObject.transform);
        }
        _shouldUpdatePathfinding = true;
    }

    private void BuildTower()
    {
        if (_tower == null)
        {
            _tower = Instantiate(_towerPrefab, transform.position, transform.rotation, gameObject.transform);
        }
        _shouldUpdatePathfinding = true;
    }

    private void DestroyWall()
    {
        // This should be replace with a Destroy method on the wall.
        Destroy(_wall);
        _shouldUpdatePathfinding = true;
    }

    private void DestroyTower()
    {
        // This should be replace with a Destroy method on the tower.
        Destroy(_tower);
        _shouldUpdatePathfinding = true;
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
        Debug.Log("ApplyCreationCurrency");
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
            DestroyWall();
            return;
        }
    }

    private void UpdatePathfinding()
    {
        _astar.Scan();
        _shouldUpdatePathfinding = false;
    }
}
