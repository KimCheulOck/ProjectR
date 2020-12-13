using UnityEngine;

public abstract class State
{
    protected Actor actor = null;
    protected BodyType bodyType = BodyType.Max;
    protected Action action = null;
    protected float waitingTime;
    protected object[] extraData;

    public virtual void Enter(Actor actor, BodyType bodyType, Action action, params object[] extraData)
    {
        this.actor = actor;
        this.bodyType = bodyType;
        this.action = action;
        this.extraData = extraData;

        actor.basicBody.SetBool(GetBoolName(), true);
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        actor.basicBody.SetBool(GetBoolName(), false);
        action.End();
        actor = null;
    }

    public bool EqualExtraData(params object[] extraData)
    {
        return this.extraData.Equals(extraData);
    } 

    public abstract bool WaitingNextState();

    protected string GetBoolName()
    {
        return string.Format("{0}_{1}Body", actor.GetStateType(bodyType), bodyType.ToString());
    }
}