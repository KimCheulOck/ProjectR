using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using Google.Protobuf;
using UnityEngine;

public class GameServer : MonoBehaviour
{
	private ServerSession serverSession = new ServerSession();
	private PacketManager packetManager = new PacketManager();

	public void CheckPacketQueue()
	{
		// 실시간으로 패킷 큐에서 호출해줄 이벤트가 있는지 체크하고
		// 이벤트가 있다면 호출시킨다.
		List<PacketMessage> list = PacketQueue.Instance.PopAll();
		foreach (PacketMessage packet in list)
		{
			Action<PacketSession, IMessage> handler = packetManager.GetPacketHandler(packet.Id);
			if (handler != null)
				handler.Invoke(serverSession, packet.Message);
		}
	}

	public void Send(IMessage packet)
	{
		serverSession.Send(packet);
	}

	private void Start()
	{
		Initialize();
		ConnectServer();
	}

	private void Initialize()
	{
		packetManager.Register();
		serverSession.SetPacketModel(packetManager);
	}

	private void ConnectServer()
	{
		// DNS (Domain Name System)
		// 로컬 컴퓨터의 호스트 이름을 가져온다.
		string host = Dns.GetHostName();

		// 호스트 이름 또는 IP 주소를 넘기면 IPHostEntry 인스턴스로 얻어온다.
		// IPHostEntry : 인터넷 호스트 주소 정보에 컨테이너 클래스를 제공
		IPHostEntry ipHost = Dns.GetHostEntry(host);

		// 호스트와 연결된 IP 주소 목록을 가져온다. [0]번째
		IPAddress ipAddr = ipHost.AddressList[0];

		// IPEndPoint : 호스트의 IP주소와 서비스의 포트 번호를 결합하여
		// 서비스에 대한 연결지점을 형성한다.
		// (서버의 주소, 포트번호)
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();
		connector.Connect(endPoint,
			() => { return serverSession; },
			1);
	}
}
