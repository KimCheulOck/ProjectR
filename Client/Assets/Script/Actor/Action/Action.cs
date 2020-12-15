using UnityEngine;

public class Action
{
    protected Actor actor;
    protected BodyType bodyType;
    protected ActionType actionType;
    protected float waitingTime;
    public object[] extraData;
    public Vector2 ClickLocation
    {
        get
        {
            // 해당 엑터가 내꺼인지 아닌지에 따라
            // 1. 내꺼, 2. 몬스터 or NPC, 3. 타 유저
            return Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            //return (Vector2)extraData[0];
        }
    }

    public Action(Actor actor, ActionType actionType)
    {
        this.actor = actor;
        this.actionType = actionType;
    }

    public virtual void Initialize(BodyType bodyType, params object[] extraData)
    {
        this.bodyType = bodyType;
        this.extraData = extraData;

        actor.basicBody.SetTrigger(string.Format("{0}_{1}Body", actionType.ToString(),  bodyType.ToString()));
    }

    public virtual void Start()
    {
    }

    public virtual void SetWatingTime(float waitingTime)
    {
        this.waitingTime = waitingTime;
    }

    public virtual bool WaitingNextState()
    {
        return false;
    }

    public virtual void End()
    {
        actor.basicBody.ResetTrigger(string.Format("{0}_{1}Body", actionType.ToString(), bodyType.ToString()));
    }

}