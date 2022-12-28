using System;
using UnityEngine;

[RequireComponent(typeof(DropEnemiesOnPlatform))]
public class MoveTowardsCenter : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _slowSpeed = 0.25f;

    [SerializeField]
    private DropEnemiesOnPlatform _dropEnemiesOnPlatform;
    private void Awake()
    {
        _dropEnemiesOnPlatform = gameObject.GetComponent<DropEnemiesOnPlatform>();
    }
    // Update is called once per frame
    void Update()
    {
        float modifiedSpeed = _speed;
        if (_dropEnemiesOnPlatform.PayloadTarget == null)
        {
            modifiedSpeed = _speed * _slowSpeed;
        }
        transform.position += -_speed * Time.deltaTime * new Vector3(transform.position.x, 0, transform.position.z);
    }
}
