using System.Collections;

public class ProfilePresenter : BasePresenter
{
    private ProfileModel model;
    private ProfileView view;

    public ProfilePresenter(params object[] param) : base(param)
    {
        model = new ProfileModel();
        model.actorInfos = new ActorInfo[param.Length];
        for (int i = 0; i < param.Length; ++i)
            model.actorInfos[i] = (ActorInfo)param[i];
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        string pid = GameManager.Instance.MyPlayer.pid;
        WebRequestLogout request = new WebRequestLogout(pid, null);
        NetworkManager.Web.SendLogout(request);

        FlowManager.Instance.BackToFlow();

        base.Exit();
    }

    public override IEnumerator LoadingProcess()
    {


        yield break;
    }

    public override void LoadingEnd()
    {
        view = CreateView<ProfileView>();
        view.AddEvent(Exit);
        view.SetCharacter(model.actorInfos);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.ProfileView;
    }
}
