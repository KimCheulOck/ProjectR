using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IObserver
{
    [SerializeField]
    private WorldController worldController = null;
    [SerializeField]
    private MonsterController monsterController = null;
    [SerializeField]
    private HUDController hudController = null;
    [SerializeField]
    private InputController inputController = null;
    [SerializeField]
    private Transform actorGroup = null;
    [SerializeField]

    private List<PlayerController> playerControllers = null;
    private PlayerController myPlayerController = null;

    void IObserver.RefrashObserver(ObserverMessage id, object[] message)
    {
        switch (id)
        {
            case ObserverMessage.SetPlayer:
                {
                    GameObject playerObject = new GameObject("myPlayerController");
                    myPlayerController = playerObject.AddComponent<PlayerController>();
                    myPlayerController.transform.SetParent(actorGroup);
                    myPlayerController.SetPlayer(GameManager.Instance.MyPlayer);
                    myPlayerController.CreateMyCharacter();
                    playerControllers.Add(myPlayerController);
                }
                break;
        }
    }

    private void Awake()
    {
        AdddObserver();
    }

    private void OnDestroy()
    {
        RemoveObserver();
    }

    private void AdddObserver()
    {
        ObserverHandler.Instance.AddObserver(ObserverMessage.SetPlayer, this);
    }

    private void RemoveObserver()
    {
        ObserverHandler.Instance.RemoveObserver(this);
    }
}
