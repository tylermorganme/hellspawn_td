using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rage : StatReservoire
{
    [SerializeField]
    float _rageGainRate = 10;

    public bool IsEnraged { get; private set; }

    public UnityEvent<Transform> OnRageFull;
    public UnityEvent<Transform> OnRageEmpty;

    public override void HandleEmpty() {
        IsEnraged = false;
        OnRageEmpty?.Invoke(transform);
    }

    public override void HandleFull()
    {
        IsEnraged = true;
        //Debug.Log("Full");
        OnRageFull?.Invoke(transform);
    }

    public override void HandleModifyCurrentValue(float newValue, float oldValue) { }

    protected override void HandleAwake() { }
    public void GainRage()
    {
        //Debug.Log("Gain Rage");
        AddToCurrent(Time.deltaTime * _rageGainRate);
    }

    public void RemoveRage()
    {
        //Debug.Log("Remove Rage");
        ClearValue();
    }
 }
