using UnityEngine;

public class Character : Actor
{
    public bool IsMy { get; private set; }

    public void Awake()
    {
        stateMachine = new StateMachine(this);
        actionMachine = new ActionMachine(this);

        ChangeState(BodyType.Up, StateType.Idle);
        ChangeState(BodyType.Low, StateType.Idle);

        tag = "Player";
    }

    public void FlagIsMyActor(bool isMy)
    {
        IsMy = isMy;
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
