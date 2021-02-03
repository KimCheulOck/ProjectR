using UnityEngine;

public class EquipView : BaseView
{
    [SerializeField]
    private CategoryButtonUnit[] categoryButtonUnits = null;

    [SerializeField]
    private EquipSlotUnit[] equipSlotUnits = null;

    private System.Action<int> onEventChangeCategory;
    private System.Action onEventClose;

    public void AddEvent(System.Action<int> onEventChangeCategory,
                         System.Action onEventClose)
    {
        this.onEventChangeCategory = onEventChangeCategory;
        this.onEventClose = onEventClose;
    }

    public void RemoveEvent()
    {
        onEventChangeCategory = null;
        onEventClose = null;
    }

    public void OnClickClose()
    {
        onEventClose();
    }

    public void SetCategory(int categoryIndex)
    {
        for (int i = 0; i < categoryButtonUnits.Length; ++i)
        {
            // 모델에서 카테고리 정보를 가져와서 켜고 끈다.
            categoryButtonUnits[i].SafeSetActive(true);
            string categoryName = StringValue.GetString((EquipCategory)i);
            categoryButtonUnits[i].SetCategory(i, categoryName, onEventChangeCategory);
            categoryButtonUnits[i].SetCategoryName();
            categoryButtonUnits[i].CategoryOnOff(i == categoryIndex);
        }
    }

    public void SetSlotList(IItem[] equips)
    {
        for (int i = 0; i < equips.Length; ++i)
        {
            equipSlotUnits[i].SetSlot(equips[i]);

            if (equips[i] == null)
                equipSlotUnits[i].Empty();
            else
                equipSlotUnits[i].Show();
        }
    }
}