using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoSingleton<FlowManager>
{
    private BaseFlow prevFlow;
    private BaseFlow currentFlow;
    private Coroutine loadingProcess;

    public void ChangeFlow(BaseFlow flow)
    {
        if (currentFlow != null)
            currentFlow.Exit();

        prevFlow = currentFlow;
        currentFlow = flow;

        currentFlow.Enter();

        if(loadingProcess != null)
            StopCoroutine(loadingProcess);
        loadingProcess = StartCoroutine(LoadingProcess());
    }

    private IEnumerator LoadingProcess()
    {
        yield return currentFlow.LoadingProcess();

        currentFlow.LoadingEnd();
    }

    private void Update()
    {
        if (currentFlow == null)
            return;
    }
}
