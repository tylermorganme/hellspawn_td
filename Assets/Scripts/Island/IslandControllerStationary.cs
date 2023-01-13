using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslandControllerStationary : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed;
    [SerializeField]
    private AstarPath _astar;

    private float _rotationVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _rotationVelocity * Time.deltaTime);
        var graph = _astar.data.gridGraph;
        graph.RelocateNodes(Vector3.zero, transform.rotation, graph.nodeSize);
    }

    public void OnMove(InputValue value)
    {
        _rotationVelocity = value.Get<Vector2>().x *_rotationSpeed;
    }
}
