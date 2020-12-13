using UnityEngine;

public class Monster : Actor
{
    public void Awake()
    {
        stateMachine = new StateMachine(this);
        actionMachine = new ActionMachine(this);

        ChangeState(BodyType.Up, StateType.Idle);
        ChangeState(BodyType.Low, StateType.Idle);

        tag = "Enemey";
    }

    private void Update()
    {
        stateMachine.Update();


        // --> AI 노드들을 우선순위에 따라 작동시킨다.
    }
}
