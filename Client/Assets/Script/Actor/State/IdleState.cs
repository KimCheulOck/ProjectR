public class IdleState : State
{
    public override void Enter(Actor actor, BodyType bodyType, Action action, params object[] extraData)
    {
        base.Enter(actor, bodyType, action, extraData);

        action.Initialize(bodyType, extraData);
    }

    public override void Update()
    {
        if (action == null)
            return;

        action.Start();
    }

    public override bool WaitingNextState()
    {
        if (action == null)
            return false;

        return action.WaitingNextState();
    }

    public override void Exit()
    {
        if (actor == null)
            return;

        base.Exit();
    }
}
