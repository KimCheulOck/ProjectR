using UnityEngine;

public class ProfileCharacterUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject objProgressGroup = null;
    [SerializeField]
    private GameObject objCreateGroup = null;
    [SerializeField]
    private CharacterController characterController = null;

    private ActorInfo actorInfo;
    private int slotIndex;

    public void Initialize(ActorInfo actorInfo, int slotIndex)
    {
        this.actorInfo = actorInfo;
        this.slotIndex = slotIndex;
    }

    public void Show()
    {
        if (actorInfo == null || !actorInfo.IsRegist)
            SetEmpty();
        else
            SetCharacter();
    }

    public void OnClickCreateCharacter()
    {
        FlowManager.Instance.ChangeFlow(new CreateCharacterFlow(slotIndex));
    }

    public void OnClickEnterGame()
    {
        if (actorInfo == null || !actorInfo.IsRegist)
        {
            Debug.Log("등록되지 않은 캐릭터 정보 입니다.");
            return;
        }

        var cEnterGame = new Google.Protobuf.Protocol.C_EnterGame();
        cEnterGame.CharacterIndex = slotIndex;
        NetworkManager.Game.Send(cEnterGame);
    }
    
    public void OnClickDeleteCharacter()
    {
        string pid = GameManager.Instance.MyPlayer.pid;
        WebRequestDeleteCharacter request = new WebRequestDeleteCharacter(pid, 
                                                                          slotIndex,
                                                                          WebResponseDeleteCharacter);
        NetworkManager.Web.SendDeleteCharacter(request);
    }

    private void SetCharacter()
    {
        characterController.SafeSetActive(true);
        characterController.CreateCharacter(actorInfo);
    }

    private void SetEmpty()
    {
        characterController.SafeSetActive(false);
    }

    private void WebResponseDeleteCharacter(BaseWebResponse response)
    {
        if (NetworkManager.Web.IsError(response.result, response.msg, true))
        {
        }
        else
        {
            NetworkManager.Web.RecvDeleteCharacter(response);
            
            //actorInfo.IsRegist = false;

            Show();
        }
    }
}
