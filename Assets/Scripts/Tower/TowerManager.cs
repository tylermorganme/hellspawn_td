using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour, IHaveTarget
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private GameObject _shooter;
    [SerializeField]
    GameObject _projectilePrefab;
    [SerializeField]
    GameObject _shotPoint;
    [SerializeField]
    float _shotsPerSecond = 1;

    float _shotTimer = 0;
    bool IsReadyToShoot => _shotTimer <= 0;
    Health _targetHealth;
    GameManager _gameManager;

    public Transform Target => _target;

    bool HasTarget => (_target != null) & (_targetHealth == null ? false : !_targetHealth.IsDead);


    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        _shotTimer -= Time.deltaTime;
        TryGetNewTarget();
        TryFaceTarget();
        if (IsReadyToShoot & HasTarget)
        {
            Shoot();
            _shotTimer = 1 / _shotsPerSecond;
        }
    }

    private void Shoot()
    {
        Instantiate(_projectilePrefab, _shotPoint.transform.position, _shotPoint.transform.rotation, _gameManager.Island.transform);
    }

    private void GetNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = float.MaxValue;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            _target = nearestEnemy.transform;
            _targetHealth = _target.GetComponent<Health>();
        }
    }

    private void TryGetNewTarget()
    {
        if (!HasTarget)
        {
            GetNewTarget();
        }
    }

    private void TryFaceTarget()
    {
        if (HasTarget)
        {
            _shooter.transform.LookAt(new Vector3(_target.position.x, _shooter.transform.position.y, _target.position.z));
        }
    }
}
