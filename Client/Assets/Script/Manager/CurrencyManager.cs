using System.Collections;
using System.Collections.Generic;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public Dictionary<CurrencyType, long> Currency = new Dictionary<CurrencyType, long>();
        
    public CurrencyManager()
    {
        Currency.Add(CurrencyType.Gold, 0);
        Currency.Add(CurrencyType.Cash, 0);
    }

    public void AddCurrency(CurrencyType currencyType, long currency)
    {
        if (!Currency.ContainsKey(currencyType))
            Currency.Add(currencyType, currency);
        else
            Currency[currencyType] += currency;
    }

    public string IconPath(CurrencyType currencyType)
    {
        return string.Format("Texture/Currency/{0}", currencyType.ToString());
    }
}
