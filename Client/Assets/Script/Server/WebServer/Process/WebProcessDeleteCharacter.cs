using Google.Protobuf.Protocol;

public partial class WebServer
{
    public void SendDeleteCharacter(WebRequestDeleteCharacter request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "profile");
        webPacket.AddField("name", "deleteCharacter");
        webPacket.AddField("pid", request.Pid);
        webPacket.AddField("slotIndex", request.SlotIndex);

        Send(webPacket);
    }

    public void RecvDeleteCharacter(BaseWebResponse baseWebResponse)
    {
        WebResponseDeleteCharacter response = (WebResponseDeleteCharacter)baseWebResponse;

        //GameManager.Instance.CreateMyPlayerCharacter(response.actorInfo, response.slotIndex);

        //FlowManager.Instance.BackToFlow();
    }
}