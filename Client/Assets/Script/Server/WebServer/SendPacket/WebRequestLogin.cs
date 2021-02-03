public class WebRequestLogin : BaseWebRequest
{
    public string ID { get; private set; }
    public string Password { get; private set; }
    public override System.Type GetResponseType { get { return typeof(WebResponseLogin);} }

    public WebRequestLogin(string id, string password, System.Action<BaseWebResponse> responseCallBack)
    {
        ID = id;
        Password = password;
        ResponseCallBack = responseCallBack;
    }
}