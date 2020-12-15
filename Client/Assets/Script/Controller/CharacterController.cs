using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Character character = null;

    private Command command = null;

    public void SetCharacter(Player player)
    {
        character.FlagIsMyActor(player.IsMy);
        character.ChangeStatus(new Status());
        character.ChangeEquip(null);

        CameraController.MainCameraMoveTarget(character.transform);
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
