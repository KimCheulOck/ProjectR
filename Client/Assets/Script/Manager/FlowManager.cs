using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoSingleton<FlowManager>
{
    private BaseFlow prevFlow;
    private BaseFlow currentFlow;

    public void ChangeFlow(BaseFlow flow)
    {
        if (currentFlow != null)
            currentFlow.Exit();

        prevFlow = currentFlow;
        currentFlow = flow;

        currentFlow.Enter();
    }

    private void Update()
    {
        if (currentFlow == null)
            return;
    }
}
