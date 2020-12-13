using UnityEngine;

public class AttackState : State
{
    public override void Enter(Actor actor, BodyType bodyType, Action action, params object[] extraData)
    {
        base.Enter(actor, bodyType, action, extraData);

        action.Initialize(bodyType, extraData);
        action.SetWatingTime(Time.time + 1.0f);
    }

    public override void Update()
    {
        if (actor == null)
            return;
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