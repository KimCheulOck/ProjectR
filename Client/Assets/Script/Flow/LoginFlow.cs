using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("LoginFlow : Enter");

        SceneManager.LoadScene("02_Login");

        // 로그인 완료 시 ProfileFlow로 이동
        FlowManager.Instance.ChangeFlow(new ProfileFlow());
    }

    public override void Exit()
    {
        Debug.Log("LoginFlow : Exit");
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
