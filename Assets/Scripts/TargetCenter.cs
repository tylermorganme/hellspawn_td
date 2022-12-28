using Pathfinding;
using UnityEngine;

public class TargetCenter : AIDestinationSetter
{
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        this.target = _gameManager.Island.transform;
    }
}
