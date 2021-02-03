public partial class WebServer
{
    public void SendRegister(WebRequestRegister request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "login");
        webPacket.AddField("name", "register");
        webPacket.AddField("id", request.Id);
        webPacket.AddField("password", request.Password);
        webPacket.AddField("pin", request.Pin.ToString());

        Send(webPacket);
    }

    public void RecvRegister(BaseWebResponse baseWebResponse)
    {
        WebResponseRegister response = (WebResponseRegister)baseWebResponse;

        Debug.Log("계정 생성 됌. 원래는 여기서 팝업으로 알림");
    }
}