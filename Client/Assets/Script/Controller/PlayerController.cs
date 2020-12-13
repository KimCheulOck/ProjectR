using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null;

    private Player player = null;

    /// <summary>
    /// 테스트용
    /// </summary>
    public void Awake()
    {
        Player player = new Player();
        player.IsMy = true;
        SetPlayer(player);
        CreateCharacter();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void CreateCharacter()
    {
        characterController.SetCharacter(player);
        characterController.CommandRegistration();
    }
}
