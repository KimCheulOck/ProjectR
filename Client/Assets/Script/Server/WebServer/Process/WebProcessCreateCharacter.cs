using Google.Protobuf.Protocol;

public partial class WebServer
{
    public void SendCreateCharacter(WebRequestCreateCharacter request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "profile");
        webPacket.AddField("name", "registerCharacter");
        webPacket.AddField("pid", request.Pid);
        webPacket.AddField("nickname", request.Nickname);
        webPacket.AddField("status", request.Status);
        webPacket.AddField("slotIndex", request.SlotIndex);

        Send(webPacket);
    }

    public void RecvCreateCharacter(BaseWebResponse baseWebResponse)
    {
        WebResponseCreateCharacter response = (WebResponseCreateCharacter)baseWebResponse;

        GameManager.Instance.CreateMyPlayerCharacter(response.actorInfo, response.slotIndex);
    }
}