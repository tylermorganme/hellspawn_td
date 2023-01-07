using UnityEngine;
using UnityEngine.Events;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField]
    private float _damagePerHit;
    [SerializeField]
    private bool _hasInfiniteDamage;
    [SerializeField]
    private float _damagePool;
    [SerializeField]
    private float _timeBetweenDamage;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private bool _disposeOnDamageDealt;

    public UnityEvent OnHit;
    public UnityEvent OnAllDamageDealt;
    public UnityEvent OnDispose;

    private float _timer;
    private float _remainingDamage;

    private bool DamageTimerActive => _timer <= 0;
    private bool HasRemainingDamage => (_remainingDamage > 0) | (_hasInfiniteDamage);

    private void Awake()
    {
        _timer = _timeBetweenDamage;
        _remainingDamage = _damagePool;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        Hit(other);
        //if (DamageTimerActive & HasRemainingDamage)
        //{
        //    if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        //    {
        //        Hit(other);
        //    }
        //}
    }

    private void Hit(Collider hitTarget)
    {
        //float _damageToApply = _hasInfiniteDamage ? _damagePerHit : Mathf.Min(_damagePerHit, _remainingDamage);
        //if (!_hasInfiniteDamage)
        //    _remainingDamage -= _damageToApply;
        //if (_remainingDamage < 0)
        //{
        //    _triggerOnAllDamageDealt.Invoke();
        //    if (_disposeOnDamageDealt)
        //    {
        //        Dispose();
        //    }
        //}
        hitTarget.GetComponent<Health>()?.TakeDamage(_damagePerHit);
        OnHit.Invoke();
        if (_disposeOnDamageDealt)
        {
            Dispose();
        }
    }

    private void Dispose()
    {
        OnDispose?.Invoke();
    }
}
