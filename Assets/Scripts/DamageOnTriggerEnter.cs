using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DamageOnTriggerEnter : MonoBehaviour
{
    [SerializeField]
    private float _damagePerHit;
    [SerializeField]
    private LayerMask _layersToCollideWith;

    public UnityEvent OnHit;
    Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TryHit(other);
    }

    public void TryHit(Collider other)
    {
        if (IsGameObjectInLayerMask(other.gameObject, _layersToCollideWith))
        {
            Hit(other);
        }
    }

    private void Hit(Collider hitTarget)
    {
        hitTarget.GetComponent<HurtBox>()?.Health.TakeDamage(_damagePerHit);
        OnHit.Invoke();
        _collider.enabled = false;
    }

    private bool IsGameObjectInLayerMask(GameObject go, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << go.gameObject.layer));
    }
}
