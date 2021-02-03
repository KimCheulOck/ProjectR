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

            case ObserverMessage.ChangeCostume:
                {
                }
                break;

            case ObserverMessage.ChangeEquip:
                {
                    myPlayerController.ChangeEquip(message[0] as Equip, isWear: (bool)message[1]);
                    if (UIController.IsOpenVIew(UIPrefabs.EquipView))
                    {
                        BasePresenter equipPresenter = UIController.GetPresenter(UIPrefabs.EquipView);
                        equipPresenter.RefreshUI();
                    }

                    if (UIController.IsOpenVIew(UIPrefabs.InventoryView))
                    {
                        BasePresenter inventoryPresenter = UIController.GetPresenter(UIPrefabs.InventoryView);
                        inventoryPresenter.RefreshUI();
                    }
                }
                break;

            //case ObserverMessage.UnWearEquip:
            //    {
            //        myPlayerController.ChangeEquip(message[0] as Equip, isWear: false);
            //        if (UIController.IsOpenVIew(UIPrefabs.EquipView))
            //        {
            //            BasePresenter equipPresenter = UIController.GetPresenter(UIPrefabs.EquipView);
            //            equipPresenter.RefashUI();
            //        }

            //        if (UIController.IsOpenVIew(UIPrefabs.InventoryView))
            //        {
            //            BasePresenter inventoryPresenter = UIController.GetPresenter(UIPrefabs.InventoryView);
            //            inventoryPresenter.RefashUI();
            //        }
            //    }
            //    break;

            case ObserverMessage.UseItem:
                {
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
        ObserverHandler.Instance.AddObserver(ObserverMessage.ChangeCostume, this);
        ObserverHandler.Instance.AddObserver(ObserverMessage.ChangeEquip, this);
        ObserverHandler.Instance.AddObserver(ObserverMessage.UnWearEquip, this);
        ObserverHandler.Instance.AddObserver(ObserverMessage.UseItem, this);
    }

    private void RemoveObserver()
    {
        ObserverHandler.Instance.RemoveObserver(this);
    }
}
