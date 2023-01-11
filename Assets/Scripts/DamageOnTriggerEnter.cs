using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DamageOnTriggerEnter : MonoBehaviour
{
    [SerializeField]
    float _damagePerHit;
    [SerializeField]
    private LayerMask _layersToCollideWith;
    [SerializeField]
    float _timeBetweenHits = 1f;

    float _damageTimer = 0;
    bool _hasHitThisFrame = false;
    bool  DamageTimerReady => _damageTimer < 0;
    


    public UnityEvent OnHit;
    Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _damageTimer -= Time.deltaTime;
        if (_hasHitThisFrame)
            _collider.enabled = false;
        if (DamageTimerReady)
            _collider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_collider.enabled)
        {
            TryHit(other);
        }
    }

    private void TryHit(Collider other)
    {
        if (IsGameObjectInLayerMask(other.gameObject, _layersToCollideWith))
        {
            Hit(other);

        }
    }

    private void Hit(Collider hitTarget)
    {
        hitTarget.GetComponent<HurtBox>()?.Health.TakeDamage(_damagePerHit);
        _damageTimer = _timeBetweenHits;
        _hasHitThisFrame = true;
        OnHit.Invoke();
    }

    private bool IsGameObjectInLayerMask(GameObject go, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << go.gameObject.layer));
    }
}
