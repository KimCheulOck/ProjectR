using System.Collections;

public class ChangePasswordPresenter : BasePresenter
{
    private ChangePasswordModel model;
    private ChangePasswordView view;

    public ChangePasswordPresenter(params object[] param) : base(param)
    {
        model = new ChangePasswordModel();
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override IEnumerator LoadingProcess()
    {
        yield break;
    }

    public override void LoadingEnd()
    {
        view = CreateView<ChangePasswordView>();
        view.AddEvent(model.CheckId,
                      model.CheckPassword,
                      model.CheckChangePassword,
                      model.CheckPin);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.ChangePasswordView;
    }
}
