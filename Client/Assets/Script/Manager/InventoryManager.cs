using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private Dictionary<ItemType, List<IItem>> itemList = new Dictionary<ItemType, List<IItem>>();

    public List<IItem> Equips { get { return itemList[ItemType.Equip]; } }
    public List<IItem> Potions { get { return itemList[ItemType.Potion]; } }
    public List<IItem> Matrials { get { return itemList[ItemType.Matrial]; } }
    public List<IItem> Costumes { get { return itemList[ItemType.Costume]; } }

    public int InventorySlotCount = 0;
    public int InventorySlotCountMax = 999;

    public InventoryManager()
    {
        for (int i = 0; i < (int)ItemType.Max; ++i)
        {
            itemList.Add((ItemType)i, new List<IItem>());
            itemList[(ItemType)i].Clear();
        }

        InventorySlotCount = 0;
    }

    public void AddInventory(ItemType itemType, IItem item)
    {
        if (itemList.ContainsKey(itemType))
        {
            itemList[itemType].Add(item);
            InventorySlotCount++;
        }
    }

    public void RemoveInventory(ItemType itemType, IItem item)
    {
        if (itemList.ContainsKey(itemType))
        {
            itemList[itemType].Remove(item);
            InventorySlotCount--;
        }
    }

    public List<IItem> GetInventoryList(InventoryCategory inventoryCategory)
    {
        switch (inventoryCategory)
        {
            case InventoryCategory.Equip:
                {
                    return Equips;
                }

            case InventoryCategory.Item:
                {
                    List<IItem> allList = new List<IItem>();
                    allList.AddRange(Potions);
                    allList.AddRange(Matrials);
                    return allList;
                }

            case InventoryCategory.Etc:
                {
                    return Costumes; 
                }

            default:
                {
                    List<IItem> allList = new List<IItem>();
                    allList.AddRange(GetInventoryList(InventoryCategory.Equip));
                    allList.AddRange(GetInventoryList(InventoryCategory.Item));
                    allList.AddRange(GetInventoryList(InventoryCategory.Etc));
                    return allList;
                }
        }
    }
}