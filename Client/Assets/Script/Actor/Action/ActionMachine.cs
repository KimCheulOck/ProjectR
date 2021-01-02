using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMachine
{
    private Action[,] actions = new Action[(int)BodyType.Max, (int)ActionType.Max];
    private ActionType[] actionTypes = new ActionType[(int)BodyType.Max];

    public ActionMachine(Actor actor)
    {
        for (int bodyIndex = 0; bodyIndex < (int)BodyType.Max; ++bodyIndex)
        {
            actions[bodyIndex, (int)ActionType.Breathing] = new BreathingAction(actor, ActionType.Breathing);
            actions[bodyIndex, (int)ActionType.Slash] = new SlashAction(actor, ActionType.Slash);
            actions[bodyIndex, (int)ActionType.Walking] = new WalkingAction(actor, ActionType.Walking);
            actions[bodyIndex, (int)ActionType.Bow] = new ShootArrowAction(actor, ActionType.Bow);
            

            actionTypes[bodyIndex] = ActionType.None;
        }
    }

    public void ChangeAction(BodyType bodyType, StateType stateType, Equip[] Equips)
    {
        int bodyIndex = (int)bodyType;

        switch (stateType)
        {
            case StateType.Move:
                {
                    actionTypes[bodyIndex] = ActionType.Walking;
                    break;
                }
            case StateType.Attack:
                {
                    AttackAction(bodyIndex, Equips[(int)EquipType.LeftWeapon]);
                    break;
                }
            case StateType.Idle:
                {
                    actionTypes[bodyIndex] = ActionType.Breathing;
                    break;
                }
        }
    }

    public Action GetAction(BodyType bodyType)
    {
        int bodyIndex = (int)bodyType;
        return actions[bodyIndex, (int)actionTypes[bodyIndex]];
    }

    private void AttackAction(int bodyIndex, Equip equip)
    {
        if (equip == null)
        {
            actionTypes[bodyIndex] = ActionType.Slash;
            return;
        }

        switch (equip.weaponType)
        {
            case WeaponType.Bow:
                {
                    actionTypes[bodyIndex] = ActionType.Bow;
                }
                break;

            case WeaponType.Sword:
                {
                    actionTypes[bodyIndex] = ActionType.Slash;
                }
                break;
        }
    }
}
