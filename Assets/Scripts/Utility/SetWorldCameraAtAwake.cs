using UnityEngine;

public class SetWorldCameraAtAwake: MonoBehaviour
{
    private void Awake()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        Camera _camera = _gameManager.Camera;
        GetComponent<Canvas>().worldCamera = _camera;
    }
}
