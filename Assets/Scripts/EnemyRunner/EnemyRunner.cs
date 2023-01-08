using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : MonoBehaviour, IHaveHealth
{

    [SerializeField]
    Health _health;

    public Health Health => _health;
    public void Dispose()
    {
        Destroy(gameObject);
    }
}
