using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace ServerCore
{
	public class Connector
	{
		private Func<Session> sessionFactory;

		public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
		{
			for (int i = 0; i < count; i++)
			{
				// AddressFamily : 사용할 주소 체계
				// Stream : 데이터 전송 방식
				// Tcp : Tcp타입
				Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.sessionFactory = sessionFactory;

				// SocketAsyncEventArgs : 비동기 소켓 작업을 완료하는데 사용되는 이벤트 핸들러
				// Completed : 비동기 작업을 완료하는 데 사용되는 이벤트
				// RemoteEndPoint : 비동기 작업의 원격 IP 끝점
				// UserToken : 이 비동기 소켓과 관련된 사용자 개체
				SocketAsyncEventArgs args = new SocketAsyncEventArgs();
				args.Completed += OnConnectCompleted;
				args.RemoteEndPoint = endPoint;
				args.UserToken = socket;

				RegisterConnect(args);
			}
		}

		private void RegisterConnect(SocketAsyncEventArgs args)
		{
			// 사용자의소켓을 얻어온다.
			Socket socket = args.UserToken as Socket;
			if (socket == null)
				return;

			// 비동기로 원격 호스트 연결을 요청한다.
			// 보류 상태가 아니면 연결 성공 메소드를 호출시킨다.
			bool pending = socket.ConnectAsync(args);
			if (pending == false)
				OnConnectCompleted(null, args);
		}

		private void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				Session session = sessionFactory.Invoke();
				session.Start(args.ConnectSocket);
				session.OnConnected(args.RemoteEndPoint);
			}
			else
			{
				Debug.Log($"OnConnectCompleted Fail: {args.SocketError}");
			}
		}
	}
}
