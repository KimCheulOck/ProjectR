public class WebRequestDeleteCharacter : BaseWebRequest
{
    public string Pid { get; private set; }
    public int SlotIndex { get; private set; }
    public override System.Type GetResponseType { get { return typeof(WebResponseDeleteCharacter); } }

    public WebRequestDeleteCharacter(string pid, int slotIndex, System.Action<BaseWebResponse> responseCallBack)
    {
        Pid = pid;
        SlotIndex = slotIndex;
        ResponseCallBack = responseCallBack;
    }
}