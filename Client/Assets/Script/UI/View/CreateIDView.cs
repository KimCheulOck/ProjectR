using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreateIDView : BaseView
{
    [SerializeField]
    private InputField inputId = null;
    [SerializeField]
    private InputField inputPassword = null;
    [SerializeField]
    private InputField inputCheckPassword = null;
    [SerializeField]
    private InputField inputPin = null;

    private System.Func<string, bool> onEventCheckId = null;
    private System.Func<string, bool> onEventCheckPassword = null;
    private System.Func<string, string, bool> onEventCheckCheckedPassword = null;
    private System.Func<string, bool> onEventCheckPin = null;
    private Coroutine coroutineCretaeId = null;


    private void OnDisable()
    {
        if (coroutineCretaeId != null)
            StopCoroutine(coroutineCretaeId);
    }

    public void AddEvent(System.Func<string,bool> onEventCheckId,
                         System.Func<string,bool> onEventCheckPassword,
                         System.Func<string, string, bool> onEventCheckCheckedPassword,
                         System.Func<string, bool> onEventCheckPin)
    {
        this.onEventCheckId = onEventCheckId;
        this.onEventCheckPassword = onEventCheckPassword;
        this.onEventCheckCheckedPassword = onEventCheckCheckedPassword;
        this.onEventCheckPin = onEventCheckPin;
    }

    public void Show()
    {
        inputId.text = string.Empty;
        inputPassword.text = string.Empty;
        inputCheckPassword.text = string.Empty;
        inputPin.text = string.Empty;
    }
        
    public void OnClickCreate()
    {
        if (onEventCheckId(inputId.text))
            return;

        if (onEventCheckPassword(inputPassword.text))
            return;

        if (onEventCheckCheckedPassword(inputPassword.text, inputCheckPassword.text))
            return;

        if (onEventCheckPin(inputPin.text))
            return;

        // PIN 번호는 계정 정보 확인용으로 사용됩니다.

        inputId.text = inputId.text.Trim();
        inputPassword.text = inputPassword.text.Trim();
        inputCheckPassword.text = inputCheckPassword.text.Trim();
        inputPin.text = inputPin.text.Trim();

        //WWWForm form = new WWWForm();
        //form.AddField("order", "register");
        //form.AddField("id", inputId.text);
        //form.AddField("password", inputPassword.text);

        //coroutineCretaeId = StartCoroutine(Post(form));
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

    //[ContextMenu("Test")]
    //public void Test()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("order", "findPassword");
    //    form.AddField("id", "11");
    //    //form.AddField("password", "11111");
    //    form.AddField("pin", "123456");

    //    coroutineCretaeId = StartCoroutine(Post(form));
    //}

    //[ContextMenu("Test2")]
    //public void Test2()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("order", "changePassword");
    //    form.AddField("id", "11");
    //    form.AddField("password", "1111122");
    //    form.AddField("pin", "123456");

    //    coroutineCretaeId = StartCoroutine(Post(form));
    //}
}