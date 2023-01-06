using System;
using UnityEngine;

[RequireComponent(typeof(EnemyDropper))]
public class MoveTowardsCenter : MonoBehaviour
{
    private EnemyDropper _enemyDropper;
    private void Awake()
    {
        _enemyDropper = gameObject.GetComponent<EnemyDropper>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(transform.position.x, 0, transform.position.z).normalized;
        transform.position += -_enemyDropper.GetSpeed() * Time.deltaTime * direction;
    }
}
