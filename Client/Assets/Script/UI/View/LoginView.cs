using UnityEngine;
using UnityEngine.UI;

public class LoginView : BaseView
{
    [SerializeField]
    private InputField inputId = null;

    [SerializeField]
    private InputField inputPs = null;

    private System.Action<string, string> onEventLogin = null;
    private System.Action<string, string, string> onEventCreateId = null;
    private System.Action onEventChangePs = null;
    private System.Action onEventExit = null;

    public void AddEvent(System.Action<string, string> onEventLogin,
                         System.Action<string, string, string> onEventCreateId,
                         System.Action onEventChangePs,
                         System.Action onEventExit)
    {
        this.onEventLogin = onEventLogin;
        this.onEventCreateId = onEventCreateId;
        this.onEventChangePs = onEventChangePs;
        this.onEventExit = onEventExit;
    }

    public void RemoveEvent()
    {
        onEventLogin = null;
        onEventCreateId = null;
        onEventChangePs = null;
        onEventExit = null;
    }

    public void Show()
    {
        inputId.text = string.Empty;
        inputPs.text = string.Empty;
    }

    public void OnClickLogin()
    {
        if (string.IsNullOrEmpty(inputId.text))
        {
            // 닉네임을 입력해주세요.
            return;
        }

        if (string.IsNullOrEmpty(inputPs.text))
        {
            // 비밀번호를 입력해주세요.
            return;
        }

        onEventLogin(inputId.text, inputPs.text);
    }

    public void OnClickCreate()
    {
        if (string.IsNullOrEmpty(inputId.text))
        {
            // 닉네임을 입력해주세요.
            return;
        }

        if (string.IsNullOrEmpty(inputPs.text))
        {
            // 비밀번호를 입력해주세요.
            return;
        }

        onEventCreateId(inputId.text, inputPs.text, "1234");
    }

    public void OnClickPSChange()
    {

    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickHome()
    {
        Application.OpenURL("https://chipmunk-plump-plump.tistory.com/");
    }

    //[ContextMenu("TestF")]
    //public void TestF()
    //{
    //    int[] newInt = new int[] { 6, 10, 2 };
    //    solution(newInt);
    //}
    //public string solution(int[] numbers)
    //{
    //}
}
