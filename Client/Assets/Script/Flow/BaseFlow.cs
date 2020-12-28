public abstract class BaseFlow
{
    protected object[] param;
    public BaseFlow(params object[] param)
    {
        this.param = param;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
