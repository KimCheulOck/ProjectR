using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;

public class PacketManager
{
	private Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	private Dictionary<ushort, Action<PacketSession, IMessage>> handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		

	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

	public void Register()
	{
        onRecv.Add((ushort)MsgId.SLogin, MakePacket<S_Login>);
        handler.Add((ushort)MsgId.SLogin, PacketHandler.S_LoginHandler);


        //onRecv.Add((ushort)MsgId.SEnterRoom, MakePacket<S_EnterRoom>);
        //handler.Add((ushort)MsgId.SEnterRoom, PacketHandler.S_EnterRoomHandler);

        //onRecv.Add((ushort)MsgId.CPlayerProfile, MakePacket<C_PlayerProfile>);
        //handler.Add((ushort)MsgId.CPlayerProfile, PacketHandler.S_SpawnHandler);
        //onRecv.Add((ushort)MsgId.CEnterRoom, MakePacket<C_EnterRoom>);
        //handler.Add((ushort)MsgId.CEnterRoom, PacketHandler.S_SpawnHandler);

        //onRecv.Add((ushort)MsgId.SEnterGame, MakePacket<S_EnterGame>);
        //handler.Add((ushort)MsgId.SEnterGame, PacketHandler.S_EnterGameHandler);		
        //onRecv.Add((ushort)MsgId.SLeaveGame, MakePacket<S_LeaveGame>);
        //handler.Add((ushort)MsgId.SLeaveGame, PacketHandler.S_LeaveGameHandler);		
        //onRecv.Add((ushort)MsgId.SSpawn, MakePacket<S_Spawn>);
        //handler.Add((ushort)MsgId.SSpawn, PacketHandler.S_SpawnHandler);		
        //onRecv.Add((ushort)MsgId.SDespawn, MakePacket<S_Despawn>);
        //handler.Add((ushort)MsgId.SDespawn, PacketHandler.S_DespawnHandler);		
        //onRecv.Add((ushort)MsgId.SMove, MakePacket<S_Move>);
        //handler.Add((ushort)MsgId.SMove, PacketHandler.S_MoveHandler);
    }

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	private	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if (CustomHandler == null)
		{
			Action<PacketSession, IMessage> action = null;
			if (handler.TryGetValue(id, out action))
				action.Invoke(session, pkt);
		}
		else
		{
			CustomHandler.Invoke(session, pkt, id);
		}
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}