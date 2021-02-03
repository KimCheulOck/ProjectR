using System.Collections;
using UnityEngine.SceneManagement;

public class ProfileFlow : BaseFlow
{
    public ProfileFlow(params object[] param) : base(param)
    {
    }

    public override void Enter()
    {
        Debug.Log("ProfileFlow : Enter");

        //C_PlayerProfile cPlayerProfile = new C_PlayerProfile();
        //cPlayerProfile.DbIndex = 0;
        //NetworkManager.Instance.Send(cPlayerProfile);

        //// 서버로부터 캐릭터 정보를 얻어온다.
        //GameManager.Instance.SetPlayer();

        //// 캐릭터 선택을 완료하면 해당 캐릭터가 마지막으로 접속한 위치의 Flow로 이동
        //FlowManager.Instance.ChangeFlow(new TownFlow());
    }

    public override void Exit()
    {
        Debug.Log("ProfileFlow : Exit");

    }

    public override void Update()
    {
    }

    public override IEnumerator LoadingProcess()
    {
        // 딤드 Async

        // 씬 로딩
        yield return SceneManager.LoadSceneAsync("03_Profile");

        // 기존 UI 제거
        UIController.Exit(UIController.GetPresenter(UIPrefabs.LoginView));

        // UI 생성
        ActorInfo[] actorInfos = GameManager.Instance.MyPlayer.AllActorInfos;
        yield return UIController.EnterAsync(new ProfilePresenter(actorInfos));
    }

    public override void LoadingEnd()
    {
        // 딤드 비활성화
    }
}
