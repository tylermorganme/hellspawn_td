using System.Collections.Generic;
using UnityEngine;

public enum Currency
{
    Destruction,
    Creation
}

public class CurrencyManager : MonoBehaviour
{
    //[HideInInspector]
    public Currency SelectedCurrency = Currency.Creation;
    private Dictionary<Currency, int> _currencyBank;

    public void AddCurrency(Currency c, int amount)
    {
        int oldValue;
        if (_currencyBank.TryGetValue(c, out oldValue))
        {
            _currencyBank[c] += amount;
        }
        else
        {
            _currencyBank[c] = amount;
        }
    }

    public bool TrySpendCurrency(Currency c, int amount)
    {
        int oldValue;
        if (_currencyBank.TryGetValue(c, out oldValue))
        {
            if (_currencyBank[c] >= amount)
            {
                _currencyBank[c] -= amount;
                return true;
            }
        }
        return false;
    }

    public void SetSelectedCurrency(Currency c)
    {
        SelectedCurrency = c;
    }

    public void SetSelectedCurrencyToCreation()
    {
        SetSelectedCurrency(Currency.Creation);
    }

    public void SetSelectedCurrencyToDestruction()
    {
        SetSelectedCurrency(Currency.Destruction);
    }
}
