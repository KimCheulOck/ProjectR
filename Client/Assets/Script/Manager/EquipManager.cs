using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public Equip[] GetWearEquips()
    {
        Equip[] wearEquips = new Equip[(int)EquipType.Max];
        List<IItem> equips = InventoryManager.Instance.Equips;
        for (int i = 0; i < equips.Count; ++i)
        {
            Equip equip = equips[i] as Equip;
            if (equip == null)
                continue;

            if (!equip.isWear)
                continue;

            wearEquips[(int)equip.equipType] = equip;
        }

        return wearEquips;
    }

    public Equip CreateEquip(EquipTableData equipTableData, bool isWear)
    {
        Equip equip = new Equip(equipTableData.equipType, equipTableData.status, isWear, equipTableData.path);
        return equip;
    }

    public void TestSetWearEquip()
    {
        Equip body = CreateEquip(EquipTable.Instance.FindToIndex(10000), true);
        Equip Hand = CreateEquip(EquipTable.Instance.FindToIndex(11000), true);
        Equip Head = CreateEquip(EquipTable.Instance.FindToIndex(12000), true);
        Equip Leg = CreateEquip(EquipTable.Instance.FindToIndex(13000), true);

        Equip rightHand = CreateEquip(EquipTable.Instance.FindToIndex(20000), true);
        rightHand.SetCount(5);
        Equip leftHand = CreateEquip(EquipTable.Instance.FindToIndex(22000), true);

        InventoryManager.Instance.AddInventory(ItemType.Equip, body);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Hand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Head);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Leg);

        InventoryManager.Instance.AddInventory(ItemType.Equip, rightHand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, leftHand);
    }

    public IItem[] GetEquipList(EquipCategory equipCategory)
    {
        switch (equipCategory)
        {
            case EquipCategory.Equip:
                {
                    return GetWearEquips();
                }

            case EquipCategory.Costume:
                {
                    return GetWearEquips();
                }

            case EquipCategory.Pat:
                {
                    return GetWearEquips();
                }

            default:
                {
                    return null;
                }
        }
    }
}
