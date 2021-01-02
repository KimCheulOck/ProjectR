using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUnit : DragAndDropUnit
{
    [SerializeField]
    private Image imgItem = null;
    [SerializeField]
    private Text txtCount = null;

    public enum SlotType
    {
        None = 0,
        Inventory,
        Equip,
    }

    private IItem item;
    private SlotType slotType = SlotType.None;

    public void SetItem(IItem item)
    {
        this.item = item;
    }

    public void SetSlotType(SlotType slotType)
    {
        this.slotType = slotType;
    }

    public void SetButton()
    {
        // 마우스를 가져다대면 툴팁 오픈
        // 장비일 경우
        // 현재 아이템의 정보를 보여준다.
        // 현재 장착중인 장비와 비교하여 능력치 상승치를 표기한다.
        // 그 외
        // 현재 아이템의 정보를 보여준다.

        // 더블 클릭 시
        // 장비일 경우
        // 장비를 장착한다.
        // 포션일 경우
        // 포션을 먹는다. (추 후에는 풀피일때 사용 못하고 채팅에 시스템메시지를 보여주도록 하자)
    }

    public void Show()
    {
        imgItem.SafeSetSprite("");
        txtCount.SafeSetText(item.Count == 0 ? string.Empty : string.Format("x{0}", item.Count));
    }

    public void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }

    public void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }

    public void OnMouseClick()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (slotType == SlotType.Inventory)
            {
                // 아이템 장착 or 교체
                switch (item.ItemType)
                {
                    case ItemType.Equip:
                        {
                            Equip equip = item as Equip;
                            if (equip.isWear)
                                return;

                            ObserverHandler.Instance.NotifyObserver(ObserverMessage.ChangeEquip, item);
                        }
                        break;

                    case ItemType.Potion:
                        {
                            ObserverHandler.Instance.NotifyObserver(ObserverMessage.UseItem, item);
                        }
                        break;

                    case ItemType.Costume:
                        {
                            ObserverHandler.Instance.NotifyObserver(ObserverMessage.ChangeCostume, item);
                        }
                        break;
                }
            }
            else if (slotType == SlotType.Equip)
            {
                // 장착 해제
                ObserverHandler.Instance.NotifyObserver(ObserverMessage.UnWearEquip, item);
            }
        }
    }
}
