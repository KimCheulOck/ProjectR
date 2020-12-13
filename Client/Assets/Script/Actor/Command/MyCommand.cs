public class MyCommand : Command
{
    public MyCommand(Actor actor) : base(actor)
    {
    }

    public override void AddCommandEvent()
    {
        InputController.onKeyEventMove = OnCommandEventMove;
        InputController.onKeyEventAttack = OnCommandEventAttack;
        InputController.onKeyEventWaiting = OnCommandEventDelay;
    }

    public override void RemoveCommandEvent()
    {
        InputController.onKeyEventMove = null;
        InputController.onKeyEventAttack = null;
    }

    protected override void OnCommandEventMove(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        actor.ChangeState(bodyType, stateType, extraData);
    }

    protected override void OnCommandEventAttack(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        actor.ChangeState(bodyType,stateType, extraData);
    }

    protected override bool OnCommandEventDelay(BodyType bodyType)
    {
        return actor.WaitingNextState(bodyType);
    }
}
