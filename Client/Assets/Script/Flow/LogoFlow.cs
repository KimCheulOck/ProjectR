using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("LogoFlow : Enter");
        // 인트로 애니메이션을 실행 시킨다.
        // 애니메이션이 끝나면 Intro 플로우로 보낸다.
        FlowManager.Instance.ChangeFlow(new IntroFlow());
    }

    public override void Exit()
    {
        Debug.Log("LogoFlow : Exit");
    }

    public override void Update()
    {
    }
}
