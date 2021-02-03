using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public partial class WebServer : MonoBehaviour
{
    private const string URL =
        "https://script.google.com/macros/s/AKfycbxuNwRjALe2fvtiKgWNfBqCG4VYW6stHk8Ybm9MgxdUREMLodAEzZcW/exec";

    private Queue<WebPacketQueue> webPacketQueue = new Queue<WebPacketQueue>();
    private Coroutine sendCoroutine = null;
    private bool IsWating = false;

    public void CheckPacketQueue()
    {
        if (IsWating)
            return;

        if (webPacketQueue.Count == 0)
            return;

        Send(webPacketQueue.Dequeue());
    }

    public void Send(WebPacketQueue webPacket)
    {
        if(IsWating)
        {
            Debug.Log("IsWating! " + JsonUtility.ToJson(webPacket));
            webPacketQueue.Enqueue(webPacket);
            return;
        }

        if (sendCoroutine != null)
            StopCoroutine(sendCoroutine);

        sendCoroutine = StartCoroutine(SendWebRequest(webPacket));
    }

    public bool IsError(WebErrorCode errorCode, string msg, bool isNotice)
    {
        if (errorCode == WebErrorCode.OK)
            return false;

        string message = "";
        switch (errorCode)
        {
            case WebErrorCode.REGISTER_FAIL_CHARACTER_LIMIT:
                {
                    message = "사용할 수 없는 아이디 입니다.";
                }
                break;

            case WebErrorCode.REGISTER_FAIL_PASSWORD_LIMIT:
                {
                    message = "사용할 수 없는 비밀번호 입니다.";
                }
                break;

            case WebErrorCode.REGISTER_FAIL_PIN_LIMIT:
                {
                    message = "사용할 수 없는 PIN번호 입니다.";
                }
                break;

            case WebErrorCode.REGISTER_FAIL_OVERLAP:
                {
                    message = "이미 사용중인 ID 입니다.";
                }
                break;

            case WebErrorCode.LOGIN_FAIL_ID:
                {
                    message = "아이디 또는 비밀번호가 일치하지 않습니다.";
                }
                break;

            case WebErrorCode.LOGIN_FAIL_PASSWORD:
                {
                    message = "아이디 또는 비밀번호가 일치하지 않습니다.";
                }
                break;

            default:
                {
                    message = "서버가 불안정하다고 하자";
                    break;
                }
        }

        if (isNotice)
        {
            // 여기서 에러 메시지를 띄운다.
            // message
            // 나중에 위에 message를 Table로 얻어오게끔 하자
        }

        return true;
    }

    private IEnumerator SendWebRequest(WebPacketQueue webPacket)
    {
        IsWating = true;

        webPacket.SendLog();
        UnityWebRequest www = UnityWebRequest.Post(URL, webPacket.GetWWWFrom());
       yield return www.SendWebRequest();

        string responseData = www.downloadHandler.text;
        Debug.Log("Recv : " + responseData);

        //var testLog = Newtonsoft.Json.JsonConvert.DeserializeObject<WebResponseProfile>(responseData);
        //Debug.Log("testLog : " + testLog);
        //Debug.Log("testLog : " + testLog.actorInfos);

        BaseWebResponse responseClass = (BaseWebResponse)JsonUtility.FromJson(responseData, webPacket.Request.GetResponseType);

        if (webPacket.Request.ResponseCallBack == null)
        {
            // 콜백이 없다면 곧바로 에러 체크
            IsError(responseClass.result, responseClass.msg, false);
        }
        else
        {
            // 콜백이 있으면 콜백 호출
            // 웹으로부터 받은 정보가 에러일 경우 해당 콜백에서 처리
            // (해당 콜백에서만 처리 가능한 작업들이 있을 수 있으므로)
            webPacket.Request.ResponseCallBack(responseClass);
        }

        IsWating = false;
    }
}