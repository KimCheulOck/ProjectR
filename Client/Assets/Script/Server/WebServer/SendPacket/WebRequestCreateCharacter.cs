public class WebRequestCreateCharacter : BaseWebRequest
{
    public string Pid { get; private set; }
    public string Nickname { get; private set; }
    public int SlotIndex { get; private set; }
    public Status Status { get; private set; }
    public override System.Type GetResponseType { get { return typeof(WebResponseLogin); } }

    public WebRequestCreateCharacter(string pid , string nickname, int slotIndex, 
        Status status,  System.Action<BaseWebResponse> responseCallBack)
    {
        Pid = pid;
        Nickname = nickname;
        Status = status;
        SlotIndex = slotIndex;
        ResponseCallBack = responseCallBack;
    }
}