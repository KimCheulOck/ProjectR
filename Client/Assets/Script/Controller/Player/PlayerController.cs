using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null;

    private Player player = null;

    /// <summary>
    /// 테스트용
    /// </summary>
    public void Start()
    {
        Player player = new Player();
        player.playerName = "다람쥐";
        player.createTime = 0;
        player.dbIndex = 0;
        player.isMy = true;
        player.status = new Status();

        SetPlayer(player);

        CreateMyCharacter();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void CreateMyCharacter()
    {
        characterController.SetCharacter(player.isMy);
        characterController.SetStatus(player.status);
        characterController.SetEquips(EquipManager.Instance.GetWearEquips());
        characterController.SetSkills(null);
        characterController.CommandRegistration();
    }
}
