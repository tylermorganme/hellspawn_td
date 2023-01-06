using UnityEngine;

internal interface IHaveSpeed
{
    public float GetSpeed();
    public float SetSpeed(float speed);
    public float ApplyFactorToSpeed(float factor);
}