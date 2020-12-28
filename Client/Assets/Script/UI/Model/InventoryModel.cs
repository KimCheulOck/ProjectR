using UnityEngine;
using System.Collections.Generic;

public class InventoryModel
{
    public List<IItem> itemList;
    public int categoryIndex;
    public int inventorySlotCount;
    public int inventorySlotCountMax;

    public void SetCategory(int categoryIndex)
    {
        this.categoryIndex = categoryIndex;
        itemList = InventoryManager.Instance.GetInventoryList((InventoryCategory)categoryIndex);

        PlayerPrefs.SetInt("InventoryCategory", categoryIndex);
    }

    public void SetSlotCount()
    {
        inventorySlotCount = InventoryManager.Instance.InventorySlotCount;
        inventorySlotCountMax = InventoryManager.Instance.InventorySlotCountMax;
    }
}