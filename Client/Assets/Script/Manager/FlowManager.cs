using System.Collections;
using UnityEngine;

public class FlowManager : MonoSingleton<FlowManager>
{
    private Stack prevFlow = new Stack();
    private BaseFlow currentFlow;
    private Coroutine loadingProcess;

    public void ChangeFlow(BaseFlow flow, bool isStack = false)
    {
        if (currentFlow != null)
        {
            if (isStack)
                prevFlow.Push(currentFlow);

            currentFlow.Exit();
        }

        currentFlow = flow;

        currentFlow.Enter();

        if(loadingProcess != null)
            StopCoroutine(loadingProcess);
        loadingProcess = StartCoroutine(LoadingProcess());
    }

    public void BackToFlow()
    {
        if (prevFlow.Count == 0)
            return;

        if (currentFlow != null)
            currentFlow.Exit();

        currentFlow = (BaseFlow)prevFlow.Pop();
        
        currentFlow.Enter();

        if (loadingProcess != null)
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
