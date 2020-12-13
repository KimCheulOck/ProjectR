public abstract class Command
{
    public abstract void AddCommandEvent();
    public abstract void RemoveCommandEvent();

    protected abstract void OnCommandEventMove(BodyType bodyType, StateType stateType, params object[] extraData);
    protected abstract void OnCommandEventAttack(BodyType bodyType, StateType stateType, params object[] extraData);
    protected abstract bool OnCommandEventDelay(BodyType bodyType);

    protected Actor actor = null;

    public Command(Actor actor)
    {
        this.actor = actor;
    }
}
