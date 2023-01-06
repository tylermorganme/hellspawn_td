using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour, IHaveTarget
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private GameObject _shooter;

    private bool HasTarget => _target != null;
    public Transform Target => _target;

    void Update()
    {
        if (!HasTarget)
        {
            GetNewTarget();
        }
        if (HasTarget)
        {
            _shooter.transform.LookAt(new Vector3(_target.position.x, _shooter.transform.position.y, _target.position.z));
        }
    }

    private void GetNewTarget()
    {
        // Find all enemies within the specified range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Set the initial minimum distance to the maximum float value
        float minDistance = float.MaxValue;

        // Set the initial nearest enemy to null
        GameObject nearestEnemy = null;

        // Loop through all enemies
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the enemy and the current game object
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            // If the distance is less than the current minimum distance, set the enemy as the nearest enemy
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // If we found a nearest enemy, do something with it (e.g. attack it)
        if (nearestEnemy != null)
        {
            _target = nearestEnemy.transform;
        }
    }
}
