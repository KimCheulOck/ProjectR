using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected EquipController equipController = null;

    public Animator basicBody = null;

    public Status Status { get; private set; }
    public Equip[] Equips { get; private set; }
    public Skill[] Skills { get; private set; }

    protected StateMachine stateMachine = null;
    protected ActionMachine actionMachine = null;
    public string ActorTag { get; protected set; }

    public void SetTag(string tag)
    {
        ActorTag = tag;
    }

    public void ChangeState(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        if (!stateMachine.PossibleChangeState(bodyType, stateType, extraData))
            return;

        actionMachine.ChangeAction(bodyType, stateType);

        Action action = actionMachine.GetAction(bodyType);
        stateMachine.ChangeState(bodyType, stateType, action, extraData);

        if (bodyType == BodyType.Up)
        {
            equipController.UseWeapon(EquipType.RightWeapon, stateType, action);
            equipController.UseWeapon(EquipType.LeftWeapon, stateType, action);
        }
    }

    public void ChangeStatus(Status stats)
    {
        Status = stats;
    }

    public void ChangeEquip(Equip[] equips)
    {
        Equips = equips;

        equipController.SetActor(this);
        equipController.SetEquip(Equips);
        equipController.SetEquipParts();
    }

    public void ChangeSkills(Skill[] skills)
    {
        Skills = skills;
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
}
