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
}
