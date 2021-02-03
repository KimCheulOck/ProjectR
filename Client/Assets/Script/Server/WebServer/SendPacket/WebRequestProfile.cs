public class WebRequestProfile : BaseWebRequest
{
    public string Pid { get; private set; }
    public override System.Type GetResponseType { get { return typeof(WebResponseProfile); } }

    public WebRequestProfile(string pid, System.Action<BaseWebResponse> responseCallBack)
    {
        Pid = pid;
        ResponseCallBack = responseCallBack;
    }
}