using System;
using UnityEngine;

[RequireComponent(typeof(EnemyDropper))]
public class DropEnemiesOnPlatform : MonoBehaviour
{
    [SerializeField]
    LayerMask _validDropLayers;
    [SerializeField]
    LayerMask _blockingDropLayers;
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    float _timeBetweenDrops = 2;
    [SerializeField]
    int _enemiesToDrop = 1;

    private GameObject _payloadTarget = null;
    private float _timer = 0;
    private GameManager _gameManager;
    public GameObject PayloadTarget => _payloadTarget;
    private int _droppedEnemies;
    private bool HasEnemiesToDrop => _droppedEnemies < _enemiesToDrop;
    public bool HasStartedDropping => _droppedEnemies > 0;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        IsAbovePlatform();
        if(_timer < 0 && HasEnemiesToDrop)
        {
            DropEnemy();
            _timer = _timeBetweenDrops;
        }
        if (!HasEnemiesToDrop)
        {
            HandleDroppedAllEnemies();
        }
    }

    private void HandleDroppedAllEnemies()
    {
        Destroy(gameObject);
    }

    void IsAbovePlatform()
    {
        // Create a ray starting from the position of gameObject1 and pointing upwards
        Ray ray = new Ray(transform.position, Vector3.down);

        // Use a RaycastHit structure to store information about the object that the ray hits
        RaycastHit hit;
        //bool rayCastHit = Physics.Raycast(ray, out hit, 1000);
        //bool isInLayersToCheck = rayCastHit ? (_layersToCheck.value & 1 << hit.collider.gameObject.layer) > 0 : false;
        //// Cast the ray and check if it hits gameObject2
        //if (rayCastHit && isInLayersToCheck)
        //{
        //    _payloadTarget = hit.collider.gameObject;
        //    //Debug.Log("Is Above platform");
        //} else
        //{
        //    _payloadTarget = null;
        //}
        bool rayCastHit = Physics.Raycast(ray, out hit, 1000, _validDropLayers);
        // Cast the ray and check if it hits gameObject2
        if (rayCastHit)
        {
            _payloadTarget = hit.collider.gameObject;
        }
        else
        {
            _payloadTarget = null;
        }
    }

    void DropEnemy()
    {
        if (_payloadTarget)
        {
            Instantiate(_enemyPrefab, _payloadTarget.transform.position, Quaternion.identity, _gameManager.Island.transform);
            _droppedEnemies++;
        }
        //I might need to come back and set the parent to the island here.

    }

}
