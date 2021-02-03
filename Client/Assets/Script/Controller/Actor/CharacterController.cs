using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Character character = null;
    private Command command = null;

    public void CreateMyCharacter(ActorInfo actorInfo)
    {
        Character loadCharacter = Resources.Load<Character>(actorInfo.Path);
        character = Instantiate(loadCharacter, transform);
        character.SetActorInfo(actorInfo);
        character.Initialize();
        CameraController.MainCameraMoveTarget(character.transform);
    }

    public void CreateCharacter(ActorInfo actorInfo)
    {
        Character loadCharacter = Resources.Load<Character>(actorInfo.Path);
        character = Instantiate(loadCharacter, transform);
        character.SetActorInfo(actorInfo);
        character.Initialize();
    }

    public void ChangeEquip(Equip equip, bool isWear)
    {
        character.ChangeEquip(equip, isWear);
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
