using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField]
    Material _newMaterial;
    [SerializeField]
    float _revertAfterTime = 0;

    private Dictionary<Renderer, Material> _originalMaterials = new Dictionary<Renderer, Material>();

    void Awake()
    {
        StoreOriginalMaterials();
    }


    void StoreOriginalMaterials()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            if (!_originalMaterials.ContainsKey(renderer))
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
        StoreOriginalMaterials();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = _newMaterial;
        }
        if (_revertAfterTime > 0)
            StartCoroutine(RevertMaterialAfterTime(_revertAfterTime));
    }

    private IEnumerator RevertMaterialAfterTime(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            RevertToOriginalMaterial();
        }
    }
}
