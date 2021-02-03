using Google.Protobuf.Protocol;

public partial class WebServer
{
    public void SendLogout(WebRequestLogout request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "login");
        webPacket.AddField("name", "logout");
        webPacket.AddField("pid", request.Pid);

        Send(webPacket);
    }

    public void RecvLogout(BaseWebResponse baseWebResponse)
    {
        //WebResponseLogout response = (WebResponseLogout)baseWebResponse;

        //GameManager.Instance.SetMyPlayerCharacters(response.actorInfos);

        //FlowManager.Instance.ChangeFlow(new ProfileFlow(), isStack: true);
    }
}