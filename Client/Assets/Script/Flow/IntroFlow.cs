using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("IntroFlow : Enter");

        LoadTable();

        SceneManager.LoadScene("01_Intro");

        // 영상 시청 완료 시 LoginFlow로 이동
        FlowManager.Instance.ChangeFlow(new LoginFlow());
    }

    public override void Exit()
    {
        Debug.Log("IntroFlow : Exit");
    }

    public override void Update()
    {
    }

    private void LoadTable()
    {
        EquipTable.Instance.LoadTable();
    }
}
