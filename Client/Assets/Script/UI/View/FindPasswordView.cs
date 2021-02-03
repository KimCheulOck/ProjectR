using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FindPasswordView : BaseView
{
    [SerializeField]
    private InputField inputId = null;
    [SerializeField]
    private InputField inputPin = null;

    private System.Func<string, bool> onEventCheckId = null;
    private System.Func<string, bool> onEventCheckPin = null;
    private Coroutine coroutineFindPassword = null;

    private void OnDisable()
    {
        if (coroutineFindPassword != null)
            StopCoroutine(coroutineFindPassword);
    }

    public void AddEvent(System.Func<string, bool> onEventCheckId,
                         System.Func<string, bool> onEventCheckPin)
    {
        this.onEventCheckId = onEventCheckId;
        this.onEventCheckPin = onEventCheckPin;
    }

    public void Show()
    {
        inputId.text = string.Empty;
        inputPin.text = string.Empty;
    }

    public void Create()
    {
        if (onEventCheckId(inputId.text))
            return;

        if (onEventCheckPin(inputPin.text))
            return;

        inputId.text = inputId.text.Trim();
        inputPin.text = inputPin.text.Trim();

        //WWWForm form = new WWWForm();
        //form.AddField("order", "CreateId");
        //form.AddField("id", inputId.text);

        //coroutineFindPassword = StartCoroutine(Post(form));
    }

    //private IEnumerator Post(WWWForm form)
    //{
    //    string url = DatabaseManager.Instance.URL;
    //    using (UnityWebRequest www = UnityWebRequest.Post(url, form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.isDone)
    //            print(www.downloadHandler.text);
    //        else
    //            print("Error");
    //    }
    //}
}
