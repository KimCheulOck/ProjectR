using Google.Protobuf;
using Google.Protobuf.Protocol;

public class PacketHandler
{
	public static void S_LoginHandler(PacketSession session, IMessage packet)
	{
		S_Login sLoginHandler = packet as S_Login;
		ServerSession serverSession = session as ServerSession;

		// 정상적으로 로그인이 되었음
		ObserverHandler.Instance.NotifyObserver(ObserverMessage.FindProfile);
	}

	public static void S_PlayerProfileHandler(PacketSession session, IMessage packet)
	{
		//S_PlayerProfile playerProfilePacket = packet as S_PlayerProfile;
		//ServerSession serverSession = session as ServerSession;

		//Debug.Log("S_PlayerProfileHandler");
		//Debug.Log(playerProfilePacket.DbIndex);
		//Debug.Log(playerProfilePacket.Characters);

		//Player player = new Player();
		//player.dbIndex = playerProfilePacket.DbIndex;
		//player.AllActorInfos = new ActorInfo[playerProfilePacket.Characters.Count];
		//for (int i = 0; i < player.AllActorInfos.Length; ++i)
		//{
		//	var characterProto = playerProfilePacket.Characters[i];
		//	ActorInfo actorInfo = new ActorInfo();
		//	actorInfo.Name = characterProto.Name;
		//	actorInfo.IsMy = true;
		//	actorInfo.Tag = "Player";
		//	actorInfo.Status = new Status();
		//	{
		//		actorInfo.Status.level = characterProto.Status.Level;
		//		actorInfo.Status.exp = characterProto.Status.Exp;
		//		actorInfo.Status.hp = characterProto.Status.Hp;
		//		actorInfo.Status.mp = characterProto.Status.Mp;
		//		actorInfo.Status.attack = characterProto.Status.Attack;
		//		actorInfo.Status.defense = characterProto.Status.Defense;
		//		actorInfo.Status.hit = characterProto.Status.Hit;
		//		actorInfo.Status.avoid = characterProto.Status.Avoid;
		//		actorInfo.Status.critical = characterProto.Status.Critical;
		//		//actorInfo.Status.atkSpeed = characterProto.Status.atkSpeed; 
		//		//actorInfo.Status.moveSpeed = characterProto.Status.MoveSpeed;
		//		//actorInfo.Status.spellSpeed = characterProto.Status.SpellSpeed;
		//		//actorInfo.Status.elemental = characterProto.Status.elemental;
		//	}

		//	player.AllActorInfos[i] = actorInfo;
		//}

		//FlowManager.Instance.ChangeFlow(new ProfileFlow(player));
	}

	public static void S_EnterRoomHandler(PacketSession session, IMessage packet)
	{
		S_EnterRoom enterGamePacket = packet as S_EnterRoom;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_EnterGameHandler");
	}



	//public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	//{
	//	S_EnterGame enterGamePacket = packet as S_EnterGame;
	//	ServerSession serverSession = session as ServerSession;

	//	Debug.Log("S_EnterGameHandler");
	//	Debug.Log(enterGamePacket.Player);
	//}

	//public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	//{
	//	S_LeaveGame leaveGamePacket = packet as S_LeaveGame;
	//	ServerSession serverSession = session as ServerSession;

	//	Debug.Log("S_LeaveGameHandler");
	//}

	//public static void S_SpawnHandler(PacketSession session, IMessage packet)
	//{
	//	S_Spawn spawnPacket = packet as S_Spawn;
	//	ServerSession serverSession = session as ServerSession;

	//	Debug.Log("S_SpawnHandler");
	//	Debug.Log(spawnPacket.Players);
	//}

	//public static void S_DespawnHandler(PacketSession session, IMessage packet)
	//{
	//	S_Despawn despawnPacket = packet as S_Despawn;
	//	ServerSession serverSession = session as ServerSession;

	//	Debug.Log("S_DespawnHandler");
	//}

	//public static void S_MoveHandler(PacketSession session, IMessage packet)
	//{
	//	S_Move movePacket = packet as S_Move;
	//	ServerSession serverSession = session as ServerSession;

	//	Debug.Log("S_MoveHandler");
	//}
}
