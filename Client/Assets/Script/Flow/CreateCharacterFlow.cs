using System.Collections;

public class CreateCharacterFlow : BaseFlow
{
    public CreateCharacterFlow(params object[] param) : base(param)
    {
    }

    public override void Enter()
    {
        Debug.Log("CreateCharacterFlow : Enter");
    }

    public override void Exit()
    {
        Debug.Log("CreateCharacterFlow : Exit");
    }

    public override void Update()
    {
    }

    public override IEnumerator LoadingProcess()
    {
        yield return UIController.EnterAsync(new CreateCharacterPresenter(param));
    }

    public override void LoadingEnd()
    {
    }
}
