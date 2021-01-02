using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected EquipController equipController = null;

    public Animator basicBody = null;
    public string ActorTag { get { return actorInfo.Tag; } }

    public Status Status { get { return actorInfo.Status; } }
    public Equip[] Equips {get { return actorInfo.Equips; } }
    public Skill[] Skills { get { return actorInfo.Skills; } }
    public Costume[] Costume { get { return actorInfo.Costume; } }
    public Costume[] DefaultCostume { get { return actorInfo.DefaultCostume; } }

    protected StateMachine stateMachine = null;
    protected ActionMachine actionMachine = null;
    protected ActorInfo actorInfo;

    public void SetActorInfo(ActorInfo actorInfo)
    {
        this.actorInfo = actorInfo;
    }

    public void ChangeState(BodyType bodyType, StateType stateType, params object[] extraData)
    {
        if (!stateMachine.PossibleChangeState(bodyType, stateType, extraData))
        {
            Action action = actionMachine.GetAction(bodyType);
            UseWeapon(bodyType, stateType, action);
            return;
        }

        actionMachine.ChangeAction(bodyType, stateType, Equips);

        Action changeAction = actionMachine.GetAction(bodyType);
        stateMachine.ChangeState(bodyType, stateType, changeAction, extraData);

        UseWeapon(bodyType, stateType, changeAction);
    }

    public void ChangeStatus(Status stats)
    {
        actorInfo.Status = stats;
    }

    public void ChangeEquip(Equip equip, bool isWear)
    {
        if (actorInfo.Equips[(int)equip.equipType] != null)
        {
            actorInfo.Equips[(int)equip.equipType].isWear = false;
            actorInfo.Equips[(int)equip.equipType] = null;
        }

        actorInfo.Equips[(int)equip.equipType] = equip;
        actorInfo.Equips[(int)equip.equipType].isWear = isWear;
        RefreshEquips();
    }

    public void RefreshEquips()
    {
        equipController.SetActor(this);
        equipController.SetParts();
    }

    public void ChangeSkills(Skill[] skills)
    {
        actorInfo.Skills = skills;
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

    private void UseWeapon(BodyType bodyType, StateType stateType, Action action)
    {
        if (bodyType == BodyType.Up)
        {
            equipController.UseWeapon(EquipType.RightWeapon, stateType, action);
            equipController.UseWeapon(EquipType.LeftWeapon, stateType, action);
        }
    }
}
