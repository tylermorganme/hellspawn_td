using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour, IHaveHealth
{
    [SerializeField]
    Health _health;
    public Health Health => _health;
}
