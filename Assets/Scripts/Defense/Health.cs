using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    float _maxHealth = 10;
    [SerializeField]
    float _currentHealth;
    [SerializeField]
    bool _isImmune = false;

    public UnityEvent<float> OnTakeDamage;
    public UnityEvent OnDie;
    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    //The return value should indicate if damage was taken.
    public bool TakeDamage(float amount)
    {
        if (!_isImmune)
        {
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            OnTakeDamage?.Invoke(amount);
            if (IsDead)
            {
                Die();
            }
            return true;
        }
        return false;
    }

    public void Die()
    {
        OnDie?.Invoke();
    }

    [ContextMenu("Take One Damage")]
    public void TakeOneDamage()
    {
        TakeDamage(1);
    }
}
