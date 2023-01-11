using UnityEngine;
using UnityEngine.Events;

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
