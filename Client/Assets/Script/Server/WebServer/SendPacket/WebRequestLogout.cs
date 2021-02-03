public class WebRequestLogout : BaseWebRequest
{
    public string Pid { get; private set; }
    public override System.Type GetResponseType { get { return typeof(WebResponseLogout); } }

    public WebRequestLogout(string pid, System.Action<BaseWebResponse> responseCallBack)
    {
        Pid = pid;
        ResponseCallBack = responseCallBack;
    }
}