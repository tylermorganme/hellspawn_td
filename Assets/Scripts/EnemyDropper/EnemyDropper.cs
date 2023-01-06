using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropEnemiesOnPlatform))]
public class EnemyDropper : MonoBehaviour, IHaveSpeed
{
    [SerializeField]
    private float _initialSpeed;
    [SerializeField]
    private float _droppingMovementSpeedFactor = 0.1f;
    private float _currentSpeed;
    private DropEnemiesOnPlatform _dropEnemiesOnPlatform;

    private void Awake()
    {
        _currentSpeed = _initialSpeed;
        _dropEnemiesOnPlatform = GetComponent<DropEnemiesOnPlatform>();
    }

    void Update()
    {
        _currentSpeed = _initialSpeed * (_dropEnemiesOnPlatform.HasStartedDropping ? _droppingMovementSpeedFactor : 1);
    }
    public float ApplyFactorToSpeed(float factor)
    {
        _currentSpeed *= factor;
        return _currentSpeed;
    }

    public float GetSpeed()
    {
        return _currentSpeed;
    }

    public float SetSpeed(float speed)
    {
        _currentSpeed = speed;
        return _currentSpeed;
    }
}
