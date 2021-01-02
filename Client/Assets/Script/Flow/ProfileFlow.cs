using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("ProfileFlow : Enter");

        SceneManager.LoadScene("03_Profile");

        // 서버로부터 캐릭터 정보를 얻어온다.
        GameManager.Instance.SetPlayer();

        // 캐릭터 선택을 완료하면 해당 캐릭터가 마지막으로 접속한 위치의 Flow로 이동
        FlowManager.Instance.ChangeFlow(new TownFlow());
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
        yield break;
    }

    public override void LoadingEnd()
    {
    }
}
