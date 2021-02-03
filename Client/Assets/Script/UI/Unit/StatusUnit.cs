using UnityEngine;
using UnityEngine.UI;

public class StatusUnit : MonoBehaviour
{
    [SerializeField]
    private StatusType statusType = StatusType.None;

    [SerializeField]
    private Text txtStatusName = null;
    [SerializeField]
    private Text txtStatusValue = null;

    private System.Action<StatusType> onEventPlus = null;
    private System.Action<StatusType> onEventMinus = null;

    public void AddEvent(System.Action<StatusType> onEventPlus,
                         System.Action<StatusType> onEventMinus)
    {
        this.onEventPlus = onEventPlus;
        this.onEventMinus = onEventMinus;
    }

    public void RemoveEvent()
    {
        onEventPlus = null;
        onEventMinus = null;
    }

    public void Show(string name, string value)
    {
        txtStatusName.SafeSetText(name);
        txtStatusValue.SafeSetText(value);
    }

    public void OnClickPlus()
    {
        onEventPlus(statusType);
    }

    public void OnClickMinus()
    {
        onEventMinus(statusType);
    }
}
