using UnityEngine;
using UnityEngine.EventSystems;

public class ApplyCurrencyOnClick : MonoBehaviour
{
    private PlatformManager _platformManager;
    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        _platformManager = GetComponent<PlatformManager>();
        if (_platformManager == null )
        {
            _platformManager = GetComponentInParent<PlatformManager>();
        }
        if (_platformManager == null)
        {
            Debug.LogError("ApplyCurrencyOnClick does not have a valid PlatformManager");
        }
    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        _platformManager.TryApplyCurrency(_currencyManager.SelectedCurrency);
    }
}
