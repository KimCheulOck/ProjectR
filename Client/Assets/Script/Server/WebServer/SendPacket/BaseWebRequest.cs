public abstract class BaseWebRequest
{
    public System.Action<BaseWebResponse> ResponseCallBack { get; protected set; }
    public abstract System.Type GetResponseType { get; }
}