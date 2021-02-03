using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WebPacketQueue
{
#if UNITY_EDITOR
    public Dictionary<string, string> sendPacketDic = new Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);
#endif

    public BaseWebRequest Request { get; private set; }
    
    private WWWForm form = new WWWForm();

    public WebPacketQueue(BaseWebRequest request)
    {
        Request = request;
    }

    public void AddField(string key, string value)
    {
        form.AddField(key, value);

#if UNITY_EDITOR
        sendPacketDic.Add(key, value);
#endif
    }

    public void AddField(string key, object value)
    {
        AddField(key, JsonUtility.ToJson(value));
    }


    public WWWForm GetWWWFrom()
    {
        return form;
    }

    public void SendLog()
    {
#if UNITY_EDITOR
        Debug.Log("Send : " + JsonConvert.SerializeObject(sendPacketDic));
#endif
    }
}