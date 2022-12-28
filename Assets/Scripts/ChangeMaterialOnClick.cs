using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ChangeMaterialOnClick : SelectableMonoBehaviour
public class ChangeMaterialOnClick : SelectableMonoBehaviour
{
    [SerializeField]
    Material _newMaterial;

    private Material _originalMaterial;
    [SerializeField]
    private Renderer _renderer;

    public override void Awake()
    {
        base.Awake();
        _originalMaterial = _renderer.material;
    }
    public override void HandleSelected(GameObject selectedGameObject)
    {
        _renderer.material = _newMaterial;
    }
    public override void HandleUnselected(GameObject selectedGameObject)
    {
        _renderer.material = _originalMaterial;
    }
}
