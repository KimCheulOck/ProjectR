using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFlow : BaseFlow
{
    public override void Enter()
    {
        Debug.Log("DungeonFlow : Enter");
    }

    public override void Exit()
    {
        Debug.Log("DungeonFlow : Exit");
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
