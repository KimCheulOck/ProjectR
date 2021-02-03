using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : BaseView
{
    [SerializeField]
    private CategoryButtonUnit[] categoryButtonUnits = null;

    [SerializeField]
    private WrapContent wrapContent = null;

    [SerializeField]
    private Text txtSlotCount = null;

    [SerializeField]
    private int lineMax = 5;

    private System.Action<int> onEventChangeCategory;
    private System.Action onEventClose;
    private List<IItem> itemList = null;

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
            string categoryName = StringValue.GetString((InventoryCategory)i);
            categoryButtonUnits[i].SetCategory(i, categoryName, onEventChangeCategory);
            categoryButtonUnits[i].SetCategoryName();
            categoryButtonUnits[i].CategoryOnOff(i == categoryIndex);
        }
    }

    public void SetSlotCount(int slot, int slotMax)
    {
        txtSlotCount.SafeSetText(string.Format("{0}/{1}", slot, slotMax));
    }

    public void SetSlotList(List<IItem> itemList)
    {
        this.itemList = itemList;

        int lineCount = ((itemList.Count - 1) / lineMax) + 1;
        if (lineCount < lineMax)
            lineCount = lineMax;

        wrapContent.SetChildItem(lineCount);
        wrapContent.SetUpdateEvent(OnUpdateItem);
        wrapContent.ResetPosition();
    }

    private void OnUpdateItem(int realIndex, GameObject child)
    {
        if (realIndex < 0)
            return;

        InventorySlotGroupUnit unit = child.GetComponent<InventorySlotGroupUnit>();
        if (unit == null)
            return;

        int start = realIndex * unit.childUnitCount;
        int end = start + unit.childUnitCount;
        int index = 0;
        for (int i = start; i < end; ++i)
        {
            if (i >= itemList.Count || itemList[i] == null)
                unit.EmptySlotUnit(index);
            else
                unit.SetSlotUnit(index, itemList[i]);

            index++;
        }
    }
}
