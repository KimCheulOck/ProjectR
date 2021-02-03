using System.Collections;
public class LoginPresenter : BasePresenter, IObserver
{
    private LoginModel model;
    private LoginView view;

    void IObserver.RefrashObserver(ObserverMessage id, object[] message)
    {
        switch (id)
        {
            case ObserverMessage.FindProfile:
                {
                    string pid = GameManager.Instance.MyPlayer.pid;
                    WebRequestProfile requestProfile = new WebRequestProfile(pid, (response) =>
                    {
                        if (NetworkManager.Web.IsError(response.result, response.msg, true))
                        {
                        }
                        else
                        {
                            NetworkManager.Web.RecvProfile(response);
                        }
                    });
                    NetworkManager.Web.SendProfile(requestProfile);
                    break;
                }
        }
    }

    public LoginPresenter(params object[] param) : base(param)
    {
        model = new LoginModel();
    }

    public override void Enter()
    {
        ObserverHandler.Instance.AddObserver(ObserverMessage.FindProfile, this);
    }

    public override void Exit()
    {
        ObserverHandler.Instance.RemoveObserver(this);
        view.RemoveEvent();
        base.Exit();
    }

    public override IEnumerator LoadingProcess()
    {
        yield break;
    }

    public override void LoadingEnd()
    {
        view = CreateView<LoginView>();
        view.AddEvent(OnEventLogin,
                      OnEventCreateId,
                      OnEventChangePs,
                      Exit);
        view.Show();
    }

    public override void RefreshUI()
    {
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.LoginView;
    }

    private void OnEventLogin(string id, string password)
    {
        WebRequestLogin request = new WebRequestLogin(id, password, WebResponseLogin);
        NetworkManager.Web.SendLogin(request);
    }

    private void OnEventCreateId(string id, string password, string pin)
    {
        WebRequestRegister request = new WebRequestRegister(id, password, pin, WebResponseRegister);
        NetworkManager.Web.SendRegister(request);
    }
    private void OnEventChangePs()
    {

    }

    private void WebResponseLogin(BaseWebResponse baseWebResponse)
    {
        if (NetworkManager.Web.IsError(baseWebResponse.result, baseWebResponse.msg, isNotice:true))
        {
        }
        else
        {
            NetworkManager.Web.RecvLogin(baseWebResponse);
        }
    }

    private void WebResponseRegister(BaseWebResponse baseWebResponse)
    {
        if (NetworkManager.Web.IsError(baseWebResponse.result, baseWebResponse.msg, isNotice: true))
        {
        }
        else
        {
            NetworkManager.Web.RecvRegister(baseWebResponse);
        }
    }

}
