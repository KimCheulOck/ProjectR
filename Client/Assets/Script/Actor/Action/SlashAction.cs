using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : Action
{
    public SlashAction(Actor actor, ActionType actionType) : base(actor, actionType)
    {
    }

    public override bool WaitingNextState()
    {
        return waitingTime >= Time.time;
    }
}
