using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    float _baseSpeed = 10;

    float _currentSpeed;
    
    void Start()
    {
        _currentSpeed = _baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _currentSpeed * Time.deltaTime * transform.forward;
    }
}
