using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField]
    Material _newMaterial;
    private Dictionary<Renderer, Material> _originalMaterials = new Dictionary<Renderer, Material>();

    void Awake()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            _originalMaterials.Add(renderer, renderer.material);
        }
    }

    public void RevertToOriginalMaterial()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material mat;
            _originalMaterials.TryGetValue(renderer, out mat);
            if (mat == null) continue; 
            renderer.material = mat;
        }
    }

    public void ChangeToNewMaterial()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = _newMaterial;
        }
    }
}
