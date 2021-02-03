public class WebRequestRegister : BaseWebRequest
{
    public string Id { get; private set; }
    public string Password { get; private set; }
    public string Pin { get; private set; }


    public override System.Type GetResponseType { get { return typeof(WebResponseProfile); } }

    public WebRequestRegister(string id, string password, string pin, System.Action<BaseWebResponse> responseCallBack)
    {
        Id = id;
        Password = password;
        Pin = pin;
        ResponseCallBack = responseCallBack;
    }
}