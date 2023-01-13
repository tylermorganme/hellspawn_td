using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslandControllerWASD : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 60;
    [SerializeField]
    AstarPath _astar;
    [SerializeField]
    float _maxSpeed = 10f;
    [SerializeField]
    float _accelerationRate;

    float _rotationVelocity;
    float _acceleration = 0;
    float _speed = 0;
    GridGraph _graph;
    float _nodeSize;

    private void Awake()
    {
        _graph = _astar.data.gridGraph;
        _nodeSize = _graph.nodeSize;
    }

    void Update()
    {
        //Translate
        _speed = Mathf.Clamp(_speed + _acceleration * _accelerationRate * Time.deltaTime, -_maxSpeed, _maxSpeed);
        //transform.position += transform.forward * _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        // Rotate
        transform.Rotate(Vector3.up * _rotationVelocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Update pathfinding
        _graph.RelocateNodes(transform.localPosition, transform.rotation, _nodeSize);
    }

    public void OnMove(InputValue value)
    {
        _rotationVelocity = value.Get<Vector2>().x * _rotationSpeed;
        _acceleration = value.Get<Vector2>().y;
    }
}
