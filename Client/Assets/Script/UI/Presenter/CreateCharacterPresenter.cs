using System.Collections;
public class CreateCharacterPresenter : BasePresenter
{
    private CreateCharacterModel model;
    private CreateCharacterView view;

    public CreateCharacterPresenter(params object[] param) : base(param)
    {
        model = new CreateCharacterModel(param);
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        FlowManager.Instance.BackToFlow();

        base.Exit();
    }

    public override IEnumerator LoadingProcess()
    {
        yield break;
    }

    public override void LoadingEnd()
    {
        view = CreateView<CreateCharacterView>();
        view.AddEvent(OnEventCreate, Exit);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.CreateCharacterView;
    }

    private void OnEventCreate(string nickname, Status status)
    {
        WebRequestCreateCharacter request = new WebRequestCreateCharacter(model.Pid,
                                                                          nickname,
                                                                          model.SlotIndex, 
                                                                          status,
                                                                          WebResponseCreateCharacter);
        NetworkManager.Web.SendCreateCharacter(request);
    }

    private void WebResponseCreateCharacter(BaseWebResponse response)
    {
        if (NetworkManager.Web.IsError(response.result, response.msg, true))
        {
        }
        else
        {
            NetworkManager.Web.RecvCreateCharacter(response);

            Exit();
        }
    }
}
