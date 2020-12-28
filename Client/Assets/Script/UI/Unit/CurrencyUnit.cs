using UnityEngine;
using UnityEngine.UI;

public class CurrencyUnit : MonoBehaviour
{
    [SerializeField]
    private CurrencyType currencyType = CurrencyType.Gold;

    [SerializeField]
    private bool isEnable = true;

    [SerializeField]
    private Text txtCurrency = null;

    [SerializeField]
    private Image imgCurrency = null;

    private void OnEnable()
    {
        if (isEnable)
            Show();
    }

    public void SetCurrency(CurrencyType currencyType)
    {
        this.currencyType = currencyType;
    }

    public void Show()
    {
        txtCurrency.SafeSetText(CurrencyManager.Instance.Currency[currencyType].ToString());
        imgCurrency.SafeSetSprite(CurrencyManager.Instance.IconPath(currencyType));
    }
}
