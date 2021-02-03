using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangePasswordView : BaseView
{
    [SerializeField]
    private InputField inputId = null;
    [SerializeField]
    private InputField inputPassword = null;
    [SerializeField]
    private InputField inputChangePassword = null;
    [SerializeField]
    private InputField inputPin = null;

    private System.Func<string, bool> onEventCheckId = null;
    private System.Func<string, bool> onEventCheckPassword = null;
    private System.Func<string, string, bool> onEventCheckChangePassword = null;
    private System.Func<string, bool> onEventCheckPin = null;
    private Coroutine coroutineChangePassword = null;

    private void OnDisable()
    {
        if (coroutineChangePassword != null)
            StopCoroutine(coroutineChangePassword);
    }

    public void AddEvent(System.Func<string, bool> onEventCheckId,
                         System.Func<string, bool> onEventCheckPassword,
                         System.Func<string, string, bool> onEventCheckChangePassword,
                         System.Func<string, bool> onEventCheckPin)
    {
        this.onEventCheckId = onEventCheckId;
        this.onEventCheckPassword = onEventCheckPassword;
        this.onEventCheckChangePassword = onEventCheckChangePassword;
        this.onEventCheckPin = onEventCheckPin;
    }

    public void Show()
    {
        inputId.text = string.Empty;
        inputPassword.text = string.Empty;
        inputChangePassword.text = string.Empty;
        inputPin.text = string.Empty;
    }

    public void Create()
    {
        if (onEventCheckId(inputId.text))
            return;

        if (onEventCheckPassword(inputPassword.text))
            return;

        if (onEventCheckChangePassword(inputPassword.text, inputChangePassword.text))
            return;

        if (onEventCheckPin(inputPin.text))
            return;

        inputId.text = inputId.text.Trim();
        inputPassword.text = inputPassword.text.Trim();
        inputChangePassword.text = inputChangePassword.text.Trim();
        inputPin.text = inputPin.text.Trim();

        //WWWForm form = new WWWForm();
        //form.AddField("order", "CreateId");
        //form.AddField("id", inputId.text);
        //form.AddField("password", inputPassword.text);

        //coroutineChangePassword = StartCoroutine(Post(form));
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
