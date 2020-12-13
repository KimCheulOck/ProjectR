public class StateMachine
{
    private Actor actor = null;
    private State[,] states = new State[(int)BodyType.Max, (int)StateType.Max];
    private StateType[] stateTypes = new StateType[(int)BodyType.Max];

    public StateMachine(Actor actor)
    {
        this.actor = actor;

        InitState();
    }

    public bool PossibleChangeState(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        int bodyIndex = (int)bodyType;
        if (stateTypes[bodyIndex] == stateType)
        {
            if (states[bodyIndex, (int)stateTypes[bodyIndex]].EqualExtraData(extraData))
                return false;
        }

        return true;
    }

    public void ChangeState(BodyType bodyType, StateType stateType, Action action, params object[] extraData)
    {
        int bodyIndex = (int)bodyType;
        if(stateTypes[bodyIndex] != StateType.None)
            states[bodyIndex,(int)stateTypes[bodyIndex]].Exit();

        stateTypes[bodyIndex] = stateType;
        states[bodyIndex, (int)stateType].Enter(actor, bodyType, action, extraData);
    }

    public bool WaitingNextState(BodyType bodyType)
    {
        int bodyIndex = (int)bodyType;
        if (stateTypes[bodyIndex] == StateType.None)
            return false;

        return states[bodyIndex, (int)stateTypes[bodyIndex]].WaitingNextState();
    }

    public StateType GetStateType(BodyType bodyType)
    {
        int bodyIndex = (int)bodyType;
        return stateTypes[bodyIndex];
    }

    public void Update()
    {
        for (int bodyIndex = 0; bodyIndex < (int)BodyType.Max; ++bodyIndex)
        {
            if (states[bodyIndex, (int)stateTypes[bodyIndex]] == null)
                continue;

            states[bodyIndex, (int)stateTypes[bodyIndex]].Update();
        }
    }

    private void InitState()
    {
        for (int bodyIndex = 0; bodyIndex < (int)BodyType.Max; ++bodyIndex)
        {
            states[bodyIndex, (int)StateType.Idle] = new IdleState();
            states[bodyIndex, (int)StateType.Dead] = new DeadState();
            states[bodyIndex, (int)StateType.Attack] = new AttackState();
            states[bodyIndex, (int)StateType.Move] = new MoveState();

            stateTypes[bodyIndex] = StateType.None;
        }
    }
}
