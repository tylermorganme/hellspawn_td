using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDisposable))]
public class DisposeAfterDelay : MonoBehaviour
{
    [SerializeField]
    float _timeUntilDispose;

    IDisposable _disposable;

    private void Start()
    {
        _disposable = GetComponent<IDisposable>();
        StartCoroutine(Dispose(_timeUntilDispose));
    }

    private IEnumerator Dispose(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            _disposable.Dispose();
        }
    }
}
