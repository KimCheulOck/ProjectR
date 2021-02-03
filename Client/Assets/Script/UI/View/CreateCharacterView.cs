using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterView : BaseView
{
    [SerializeField]
    private InputField inputNickName = null;

    [SerializeField]
    private StatusInfoUnit statusInfoUnit = null;

    [SerializeField]
    private CharacterController characterController = null;

    [SerializeField]
    private GameObject btnCreateDim = null;

    private CharacterTableData characterTable = null;
    private System.Action<string, Status> onEventCreate = null;
    private System.Action onEventExit = null;
    private Status status = null;
    private Status addStatus = null;

    public void AddEvent(System.Action<string, Status> onEventCreate,
                         System.Action onEventExit)
    {
        this.onEventCreate = onEventCreate;
        this.onEventExit = onEventExit;
    }

    public void Show()
    {
        SetCharacter();
        SetStatusUI();
        SetCreateButton(isDim: true);
    }

    public void OnClickCreate()
    {
        if (!statusInfoUnit.IsComplete())
        {
            // 능력치를 모두 올려주세요. (메이플 참조해보자)
            return;
        }

        Status finalStatus = new Status();
        for (int i = (int)StatusType.Hp; i < (int)StatusType.Max; ++i)
        {
            StatusType statusType = (StatusType)i;
            int value = status.GetStatus(statusType);
            int addValue = addStatus.GetStatus(statusType);
            finalStatus.SetStatus(statusType, (value + addValue));
        }

        onEventCreate(inputNickName.text, finalStatus);
    }

    public void OnClickExit()
    {
        onEventExit();
    }

    private void SetCharacter()
    {
        characterTable = CharacterTable.Instance.FindToIndex(1);
        ActorModel actorModel = new ActorModel();
        ActorInfo actorInfo = actorModel.CreateTableActor(characterTable);
        characterController.CreateCharacter(actorInfo);
    }

    private void SetStatusUI()
    {
        int configValue = 10;
        statusInfoUnit.Initialize(characterTable.status, configValue);
        statusInfoUnit.AddEvent(OnEventChangeStatus);
        statusInfoUnit.Show();
    }

    private void SetCreateButton(bool isDim)
    {
        btnCreateDim.SafeSetActive(isDim);
    }

    private void OnEventChangeStatus(Status status, Status addStatus)
    {
        this.status = status;
        this.addStatus = addStatus;

        SetCreateButton(isDim: !statusInfoUnit.IsComplete());
    }
}
