using UnityEngine;

public class PlayerController : MonoBehaviour//, IObserver
{
    [SerializeField]
    private CharacterController characterController = null;

    private Player player = null;

    //void IObserver.RefrashObserver(ObserverMessage id, object[] message)
    //{
    //    switch (id)
    //    {
    //        case ObserverMessage.ChangeCostume:
    //            {
    //            }
    //            break;

    //        case ObserverMessage.ChangeEquip:
    //            {
    //                characterController.ChangeEquip(message[0] as Equip, isWear: true);
    //            }
    //            break;

    //        case ObserverMessage.UnWearEquip:
    //            {
    //                characterController.ChangeEquip(message[0] as Equip, isWear: false);
    //            }
    //            break;

    //        case ObserverMessage.UseItem:
    //            {
    //            }
    //            break;
    //    }
    //}

    //private void Awake()
    //{
    //    AdddObserver();
    //}

    //private void OnDestroy()
    //{
    //    RemoveObserver();
    //}

    //private void AdddObserver()
    //{
    //    ObserverHandler.Instance.AddObserver(ObserverMessage.ChangeCostume, this);
    //    ObserverHandler.Instance.AddObserver(ObserverMessage.ChangeEquip, this);
    //    ObserverHandler.Instance.AddObserver(ObserverMessage.UnWearEquip, this);
    //    ObserverHandler.Instance.AddObserver(ObserverMessage.UseItem, this);
    //}

    //private void RemoveObserver()
    //{
    //    ObserverHandler.Instance.RemoveObserver(this);
    //}

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void CreateMyCharacter()
    {
        GameObject characterObject = new GameObject("characterController");
        characterController = characterObject.AddComponent<CharacterController>();
        characterController.transform.SetParent(transform);
        characterController.CreateMyCharacter(player.SelectActorInfo);
        characterController.CommandRegistration();
    }

    public void ChangeEquip(Equip equip, bool isWear)
    {
        characterController.ChangeEquip(equip, isWear);
    }
}
