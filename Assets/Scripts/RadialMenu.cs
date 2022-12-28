using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    private Camera _camera;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_camera.transform);
    }
}
