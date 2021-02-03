using Google.Protobuf.Protocol;

public partial class WebServer
{
    public void SendLogin(WebRequestLogin request)
    {
        WebPacketQueue webPacket = new WebPacketQueue(request);
        webPacket.AddField("order", "login");
        webPacket.AddField("name", "login");
        webPacket.AddField("id", request.ID);
        webPacket.AddField("password", request.Password);

        Send(webPacket);
    }

    public void RecvLogin(BaseWebResponse baseWebResponse)
    {
        WebResponseLogin response = (WebResponseLogin)baseWebResponse;

        GameManager.Instance.LoginMyPlayer(response.pid);

        // 게임 서버에 로그인 요청
        C_Login cLogin = new C_Login();
        cLogin.Pid = response.pid;
        NetworkManager.Game.Send(cLogin);
    }
}