using UnityEngine;
using UnityEngine.Events;

// Might need to add a check to this to make sure max is always higher than min because it could cause off side effects.
public abstract class StatReservoire : MonoBehaviour
{
    [SerializeField]
    private float _minValue = 0f;
    [SerializeField]
    private float _maxValue;
    [SerializeField]
    private float _initialValue;
    [SerializeField]
    private float _currentValue;

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;
    public float ValueRange => _maxValue - _minValue;

    private void Awake()
    {
        _currentValue = GetClampedValue(_initialValue);
        HandleAwake();
    }

    protected abstract void HandleAwake();

    protected virtual void AddToMax(float value)
    {
        _maxValue += value;
    }

    protected virtual void AddToMin(float value)
    {
        _minValue += value;
    }

    protected virtual void AddToCurrent(float value)
    {
        float oldValue = _currentValue;
        _currentValue = GetClampedValue(_currentValue + value);
        HandleModifyCurrentValue(_currentValue, oldValue);
        if (_currentValue != oldValue)
        {
            if (_currentValue == _minValue)
                HandleEmpty();
            if (_currentValue == _maxValue)
                HandleFull();
        }
    }

    protected virtual void ClearValue()
    {
        _currentValue = _minValue;
        HandleEmpty();
    }

    public virtual void ApplyFactor(float value, bool applyToMin, bool applyToCurrent, bool applyToMax)
    {
        if (applyToMin)
            _minValue *= value;
        if (applyToMax)
            _maxValue *= value;
        if (applyToCurrent)
            _currentValue *= value;
    }

    public float GetClampedValue(float value)
    {
        return Mathf.Clamp(_currentValue + value, _minValue, _maxValue);
    }

    public abstract void HandleFull();
    public abstract void HandleEmpty();
    public abstract void HandleModifyCurrentValue(float newValue, float oldValue);
}
