using UnityEngine;

public class Actor : MonoBehaviour
{
    public Animator basicBody = null;

    public Status Status { get; private set; }
    public Equip Equip { get; private set; }

    [SerializeField]
    protected Weapon leftWeapon = null;
    [SerializeField]
    protected Weapon rightWeapon = null;

    protected StateMachine stateMachine = null;
    protected ActionMachine actionMachine = null;
    protected string tag = "";

    public void ChangeState(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        if (!stateMachine.PossibleChangeState(bodyType, stateType, extraData))
            return;

        actionMachine.ChangeAction(bodyType, stateType);

        Action action = actionMachine.GetAction(bodyType);
        stateMachine.ChangeState(bodyType, stateType, action, extraData);

        UseWeapon(leftWeapon, stateType, action);
        UseWeapon(rightWeapon, stateType, action);
    }

    public void ChangeStatus(Status stats)
    {
        Status = stats;
    }

    public void ChangeEquip(Equip equip)
    {
        Equip = equip;
    }

    public bool WaitingNextState(BodyType bodyType)
    {
        return stateMachine.WaitingNextState(bodyType);
    }

    public StateType GetStateType(BodyType bodyType)
    {
        return stateMachine.GetStateType(bodyType);
    }

    public void Hit(Status attackerStatus)
    {
        int damage = attackerStatus.attack - Status.defense;
        if (damage < 0)
            damage = 0;

        Status.hp -= damage;
        if (Status.hp < 0)
        {
            Status.hp = 0;
        }

        Debug.Log("damage : " + damage + ", hp : " + Status.hp);
    }

    private void UseWeapon(Weapon weapon, StateType stateType, Action action)
    {
        if (weapon == null)
            return;

        if (stateType == StateType.Attack)
        {
            weapon.Initialize(tag, Status);
            weapon.Use(action);
        }
        else
        {
            weapon.Cancel();
        }
    }
}
