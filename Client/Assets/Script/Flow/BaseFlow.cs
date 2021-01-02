using System.Collections;

public abstract class BaseFlow
{
    public bool IsLoading { get; protected set; }
    protected object[] param;
    public BaseFlow(params object[] param)
    {
        this.param = param;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public abstract IEnumerator LoadingProcess();
    public abstract void LoadingEnd();
}
