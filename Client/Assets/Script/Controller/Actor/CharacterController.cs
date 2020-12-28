using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Character character = null;

    private Command command = null;

    public void SetCharacter(bool isMy)
    {
        character.FlagIsMyActor(isMy);
        character.SetTag("Player");

        CameraController.MainCameraMoveTarget(character.transform);
    }

    public void SetStatus(Status status)
    {
        character.ChangeStatus(status);
    }

    public void SetEquips(Equip[] equips)
    {
        character.ChangeEquip(equips);
    }

    public void SetSkills(Skill[] skills)
    {
        character.ChangeSkills(skills);
    }

    public void CommandRegistration()
    {
        if (character.IsMy)
        {
            command = new MyCommand(character);
        }
        else
        {
            command = new OtherCommand(character);
        }

        command.AddCommandEvent();
    }
}
