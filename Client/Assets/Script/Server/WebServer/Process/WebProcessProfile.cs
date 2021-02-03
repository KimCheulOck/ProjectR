using Google.Protobuf.Protocol;

public partial class WebServer
{
    public void SendProfile(WebRequestProfile request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "profile");
        webPacket.AddField("name", "findProfile");
        webPacket.AddField("pid", request.Pid);

        Send(webPacket);
    }

    public void RecvProfile(BaseWebResponse baseWebResponse)
    {
        WebResponseProfile response = (WebResponseProfile)baseWebResponse;

        GameManager.Instance.SetMyPlayerCharacters(response.actorInfos);

        FlowManager.Instance.ChangeFlow(new ProfileFlow(), isStack: true);
    }
}