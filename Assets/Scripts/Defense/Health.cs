using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    //The return value should indicate if damage was taken.
    public bool TakeDamage(float amount)
    {
        if (!_isImmune)
        {
            Debug.Log("Take Damage");
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            OnTakeDamage?.Invoke(amount);
            if (CurrentHealth <= 0)
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
