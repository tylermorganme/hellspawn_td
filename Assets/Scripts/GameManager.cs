using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I should make this a Singleton at some point.
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _island;
    [SerializeField]
    Camera _camera;
    private GameObject _selection;
    [SerializeField]
    private bool _shouldLogSelection = true;

    public GameObject Island => _island;
    public Camera Camera => _camera;

    [SerializeField]
    private SOGameObjectChannel _onSelectionChanged;

    public void Awake()
    {
        _onSelectionChanged.OnEventRaised += SetSelection;
    }

    public void OnDestroy()
    {
        _onSelectionChanged.OnEventRaised -= SetSelection;
    }

    private void SetSelection(GameObject obj)
    {
        _selection = obj;
        if (_shouldLogSelection)
            Debug.Log(_selection.GetInstanceID());
    }

    public GameObject Selection
    {
        get => _selection;
    }

    public void ClearSelection()
    {
        _selection = null;
        _onSelectionChanged.RaiseEvent(_selection);
    }

}
