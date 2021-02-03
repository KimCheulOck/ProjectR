using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected EquipController equipController = null;

    public Animator basicBody = null;
    public Status Status { get { return actorInfo.Status; } }
    public Equip[] Equips {get { return actorInfo.Equips; } }
    public Skill[] Skills { get { return actorInfo.Skills; } }
    public Costume[] Costume { get { return actorInfo.Costume; } }
    public Costume[] DefaultCostume { get { return actorInfo.DefaultCostume; } }
    public string ActorTag { get { return actorInfo.Tag; } }
    public bool IsMy { get { return actorInfo.IsMy; } }

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
        if (actorInfo == null)
            return;

        actorInfo.Status = stats;
    }

    public void ChangeEquip(Equip equip, bool isWear)
    {
        if (actorInfo == null)
            return;

        if (actorInfo.Equips[(int)equip.EquipType] != null)
        {
            //if (isWear && actorInfo.Equips[(int)equip.equipType].SerialIndex == equip.SerialIndex)
            //    return;   

            actorInfo.Equips[(int)equip.EquipType].IsWear = false;
            actorInfo.Equips[(int)equip.EquipType] = null;
            equipController.SetParts();
        }

        if (!isWear)
            return;

        actorInfo.Equips[(int)equip.EquipType] = equip;
        actorInfo.Equips[(int)equip.EquipType].IsWear = isWear;
        RefreshEquips();
    }

    public void RefreshEquips()
    {
        if (actorInfo == null)
            return;

        equipController.SetMyEquip(IsMy);
        equipController.SetEquip(Equips);
        equipController.SetCostume(Costume, DefaultCostume);
        equipController.SetEvent(Hit, ()=> { return transform.position; });
        equipController.SetParts();
    }

    public void ChangeSkills(Skill[] skills)
    {
        if (actorInfo == null)
            return;

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

    public bool Hit(Actor targetActor)
    {
        if (targetActor == null)
            return false;

        bool isAttack = (ActorTag == "Player" && targetActor.tag == "Enemy") ||
                        (ActorTag == "Enemy" && targetActor.tag == "Player");

        if (!isAttack)
            return false;

        int damage = targetActor.Status.attack - Status.defense;
        if (damage < 0)
            damage = 0;

        Status.hp -= damage;
        if (Status.hp < 0)
        {
            Status.hp = 0;
        }

        Debug.Log("damage : " + damage + ", hp : " + Status.hp);

        return true;
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
