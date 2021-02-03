using UnityEngine;

public class NetworkManager : MonoSingleton<NetworkManager>
{
	public static WebServer Web { get; private set; }
	public static GameServer Game { get; private set; }

	private void Start()
	{
		CreateWebInstance();
		CreateGameServerInstance();
	}

	private void CreateWebInstance()
	{
		Web = new GameObject(typeof(WebServer).ToString()).AddComponent<WebServer>();
		Web.transform.SetParent(transform);
	}

	private void CreateGameServerInstance()
	{
		Game = new GameObject(typeof(GameServer).ToString()).AddComponent<GameServer>();
		Game.transform.SetParent(transform);
	}

	private void Update()
	{
		Game.CheckPacketQueue();
		Web.CheckPacketQueue();
	}
}
