using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("TownFlow : Enter");

        // 타운인데 어느 타운인지에 따라 씬 로딩
    }

    public override void Exit()
    {
        Debug.Log("TownFlow : Exit");
    }

    public override void Update()
    {
    }

    public override IEnumerator LoadingProcess()
    {
        yield return SceneManager.LoadSceneAsync("00_Square");
    }

    public override void LoadingEnd()
    {
        ObserverHandler.Instance.NotifyObserver(ObserverMessage.SetPlayer);
    }
}
