using System.Collections;

public class CreateIDPresenter : BasePresenter
{
    private CreateIDModel model;
    private CreateIDView view;

    public CreateIDPresenter(params object[] param) : base(param)
    {
        model = new CreateIDModel();
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
        view = CreateView<CreateIDView>();
        view.AddEvent(model.CheckId,
                      model.CheckPassword,
                      model.CheckCheckedPassword,
                      model.CheckPin);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.CreateIDView;
    }
}
