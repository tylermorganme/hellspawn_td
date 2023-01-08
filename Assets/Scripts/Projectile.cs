using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDisposable
{
    public void Dispose()
    {
        Destroy(gameObject);
    }
}
