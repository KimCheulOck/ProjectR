public class OtherCommand : Command
{
    public OtherCommand(Actor actor) : base(actor)
    {
    }

    public override void AddCommandEvent()
    {
    }

    public override void RemoveCommandEvent()
    {
    }

    protected override void OnCommandEventMove(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        actor.ChangeState(bodyType, stateType);
    }

    protected override void OnCommandEventAttack(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        actor.ChangeState(bodyType, stateType);
    }

    protected override bool OnCommandEventDelay(BodyType bodyType)
    {
        return false;
    }
}
