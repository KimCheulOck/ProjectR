using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public abstract class Session
{
	private Socket socket;
	private int disconnected = 0;

	private RecvBuffer recvBuffer = new RecvBuffer(65535);

	private object objLock = new object();
	private Queue<ArraySegment<byte>> sendQueue = new Queue<ArraySegment<byte>>();
	private List<ArraySegment<byte>> pendingList = new List<ArraySegment<byte>>();
	private SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
	private SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();

	public abstract void OnConnected(EndPoint endPoint);
	public abstract int OnRecv(ArraySegment<byte> buffer);
	public abstract void OnSend(int numOfBytes);
	public abstract void OnDisconnected(EndPoint endPoint);


	public void Start(Socket socket)
	{
		this.socket = socket;

		recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
		sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

		RegisterRecv();
	}

	public void Send(List<ArraySegment<byte>> sendBuffList)
	{
		if (sendBuffList.Count == 0)
			return;

		lock (objLock)
		{
			foreach (ArraySegment<byte> sendBuff in sendBuffList)
				sendQueue.Enqueue(sendBuff);

			if (pendingList.Count == 0)
				RegisterSend();
		}
	}

	public void Send(ArraySegment<byte> sendBuff)
	{
		lock (objLock)
		{
			sendQueue.Enqueue(sendBuff);
			if (pendingList.Count == 0)
				RegisterSend();
		}
	}

	public void Disconnect()
	{
		if (Interlocked.Exchange(ref disconnected, 1) == 1)
			return;

		OnDisconnected(socket.RemoteEndPoint);
		socket.Shutdown(SocketShutdown.Both);
		socket.Close();
		Clear();
	}
	private void Clear()
	{
		lock (objLock)
		{
			sendQueue.Clear();
			pendingList.Clear();
		}
	}

	private void RegisterSend()
	{
		// 연결이 해제되었으면 빠져나온다.
		if (disconnected == 1)
			return;

		// 서버에 패킷 보낼게 있다면 보류 리스트에 담는다.
		while (sendQueue.Count > 0)
		{
			ArraySegment<byte> buff = sendQueue.Dequeue();
			pendingList.Add(buff);
		}

		// 버퍼에 보류 리스트를 담는다.
		sendArgs.BufferList = pendingList;

		try
		{
			// 서버에 보류 리스트를 보낸다.
			// 성공하면 보내기 성공 메소드를 호출한다.
			bool pending = socket.SendAsync(sendArgs);
			if (pending == false)
				OnSendCompleted(null, sendArgs);
		}
		catch (Exception e)
		{
			Debug.Log($"RegisterSend Failed {e}");
		}
	}

	private void OnSendCompleted(object sender, SocketAsyncEventArgs args)
	{
		lock (objLock)
		{
			// BytesTransferred : 인터럽트 발생 전에 전송에 성공한 바이트 수
			if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
			{
				try
				{
					// 서버로 보내기가 성공하였으면
					// 버퍼에 담아놨던 보류리스트를 없앤다.
					// 보류리스트도 초기화한다.
					sendArgs.BufferList = null;
					pendingList.Clear();

					// 보내기 성공했을 때 호출하는 메소드
					OnSend(sendArgs.BytesTransferred);

					// 서버에 보낼 패킷이 남아있으면 다시 보낸다.
					if (sendQueue.Count > 0)
						RegisterSend();
				}
				catch (Exception e)
				{
					Debug.Log($"OnSendCompleted Failed {e}");
				}
			}
			else
			{
				Disconnect();
			}
		}
	}

	private	void RegisterRecv()
	{
		// 연결이 해제되어 있으면 빠져나온다.
		if (disconnected == 1)
			return;

		// 서버로부터 받은 패킷의 버퍼를 정리한다.
		recvBuffer.Clean();
		ArraySegment<byte> segment = recvBuffer.WriteSegment;
		recvArgs.SetBuffer(segment.Array, segment.Offset, segment.Count);

		try
		{
			// 서버로부터 받은게 있다면 성공 메소드를 호출한다.
			bool pending = socket.ReceiveAsync(recvArgs);
			if (pending == false)
				OnRecvCompleted(null, recvArgs);
		}
		catch (Exception e)
		{
			Debug.Log($"RegisterRecv Failed {e}");
		}
	}

	private void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
	{
		if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
		{
			try
			{
				// Write 커서 이동
				if (recvBuffer.OnWrite(args.BytesTransferred) == false)
				{
					Disconnect();
					return;
				}

				// 컨텐츠 쪽으로 데이터를 넘겨주고 얼마나 처리했는지 받는다
				int processLen = OnRecv(recvBuffer.ReadSegment);
				if (processLen < 0 || recvBuffer.DataSize < processLen)
				{
					Disconnect();
					return;
				}

				// Read 커서 이동
				if (recvBuffer.OnRead(processLen) == false)
				{
					Disconnect();
					return;
				}

				RegisterRecv();
			}
			catch (Exception e)
			{
				Debug.Log($"OnRecvCompleted Failed {e}");
			}
		}
		else
		{
			Disconnect();
		}
	}
}
