using System.Collections;

public class FindPasswordPresenter : BasePresenter
{
    private FindPasswordModel model;
    private FindPasswordView view;

    public FindPasswordPresenter(params object[] param) : base(param)
    {
        model = new FindPasswordModel();
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
        view = CreateView<FindPasswordView>();
        view.AddEvent(model.CheckId,
                      model.CheckPin);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.FindPasswordView;
    }
}
