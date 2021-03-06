﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : Action
{
    public DeadAction(Actor actor, ActionType actionType) : base(actor, actionType)
    {
    }

    public override bool WaitingNextState()
    {
        return waitingTime >= Time.time;
    }
}
