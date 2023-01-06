using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNegativeCamera : MonoBehaviour
{

    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<GameManager>().Camera;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _camera.transform.position);
    }
}
