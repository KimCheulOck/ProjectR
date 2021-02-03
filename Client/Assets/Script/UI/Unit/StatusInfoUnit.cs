using UnityEngine;

public class StatusInfoUnit : MonoBehaviour
{
    [SerializeField]
    private StatusUnit[] statusUnits = null;

    private System.Action<Status, Status> onEventChangeStatus = null;
    private Status status = null;
    private Status addStatus = null;
    private int orizinalStatusPoint = 0;
    private int statusPoint = 0;

    public void Initialize(Status status, int statusPoint)
    {
        this.status = status;
        this.addStatus = new Status();
        this.orizinalStatusPoint = statusPoint;
        this.statusPoint = 0;
    }

    public void AddEvent(System.Action<Status, Status> onEventChangeStatus)
    {
        this.onEventChangeStatus = onEventChangeStatus;

        for (int i = 0; i < statusUnits.Length; ++i)
            statusUnits[i].AddEvent(OnEventPlus, OnEventMinus);
    }

    public void Show()
    {
        SetStatusUI(StatusType.Hp);
        SetStatusUI(StatusType.Mp);
        SetStatusUI(StatusType.Attack);
        SetStatusUI(StatusType.Defense);
        SetStatusUI(StatusType.Hit);
        SetStatusUI(StatusType.Avoid);
        SetStatusUI(StatusType.Critical);
        SetStatusUI(StatusType.AttackSpeed);
        SetStatusUI(StatusType.MoveSpeed);
        SetStatusUI(StatusType.SpellSpeed);
        SetStatusUI(StatusType.Elemental);
    }

    public bool IsComplete()
    {
        return orizinalStatusPoint == statusPoint;
    }

    private void SetStatusUI(StatusType statusType)
    {
        string name = StringValue.GetString(statusType);
        int value = status.GetStatus(statusType);
        int addValue = addStatus.GetStatus(statusType);

        if (addValue > 0)
            statusUnits[(int)statusType].Show(name, string.Format("<color=#00FF00>{0}</color>", (value + addValue)));
        else
            statusUnits[(int)statusType].Show(name, value.ToString());

    }

    private void OnEventPlus(StatusType statusType)
    {
        if (statusPoint >= orizinalStatusPoint)
            return;

        statusPoint++;

        int addValue = addStatus.GetStatus(statusType) + 1;
        addStatus.SetStatus(statusType, addValue);
        SetStatusUI(statusType);
        onEventChangeStatus(status, addStatus);
    }

    private void OnEventMinus(StatusType statusType)
    {
        if (statusPoint <= 0)
            return;

        statusPoint--;

        int addValue = addStatus.GetStatus(statusType) - 1;
        if (addValue < 0)
            return;

        addStatus.SetStatus(statusType, addValue);
        SetStatusUI(statusType);
        onEventChangeStatus(status, addStatus);
    }
}
