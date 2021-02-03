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

            if (!equip.IsWear)
                continue;

            wearEquips[(int)equip.EquipType] = equip;
        }

        return wearEquips;
    }

    public Costume[] GetCostumes()
    {
        Costume[] wearCostumes = new Costume[(int)CostumeType.Max];
        List<IItem> costumes = InventoryManager.Instance.Costumes;
        for (int i = 0; i < costumes.Count; ++i)
        {
            Costume costume = costumes[i] as Costume;
            if (costume == null)
                continue;

            if (!costume.IsWear)
                continue;

            wearCostumes[(int)costume.CostumeType] = costume;
        }

        return wearCostumes;
    }

    public Equip CreateEquip(EquipTableData equipTableData, int count, bool isWear)
    {
        Equip equip = new Equip(0, equipTableData.Index,
            equipTableData.Path, equipTableData.Thumbnail,
            equipTableData.Status, equipTableData.EquipType, equipTableData.WeaponType);
        equip.SetCount(count);
        equip.SetWear(isWear);
        return equip;
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
                    return GetCostumes();
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

    public void TestSetWearEquip()
    {
        Equip body = CreateEquip(EquipTable.Instance.FindToIndex(10000), 1, true);
        Equip Hand = CreateEquip(EquipTable.Instance.FindToIndex(11000), 1, true);
        Equip Head = CreateEquip(EquipTable.Instance.FindToIndex(12000), 1, true);
        Equip Leg = CreateEquip(EquipTable.Instance.FindToIndex(13000), 1, true);

        Equip rightHand = CreateEquip(EquipTable.Instance.FindToIndex(20000), 5, true);
        Equip leftHand = CreateEquip(EquipTable.Instance.FindToIndex(22000), 1, true);

        InventoryManager.Instance.AddInventory(ItemType.Equip, body);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Hand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Head);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Leg);

        InventoryManager.Instance.AddInventory(ItemType.Equip, rightHand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, leftHand);
    }

    public void TestSetEquip()
    {
        Equip body = CreateEquip(EquipTable.Instance.FindToIndex(10000), 1, false);
        Equip Hand = CreateEquip(EquipTable.Instance.FindToIndex(11000), 1, false);
        Equip Head = CreateEquip(EquipTable.Instance.FindToIndex(12000), 1, false);
        Equip Leg = CreateEquip(EquipTable.Instance.FindToIndex(13000), 1, false);

        Equip rightHand = CreateEquip(EquipTable.Instance.FindToIndex(20000), 5, false);
        Equip leftHand = CreateEquip(EquipTable.Instance.FindToIndex(22000), 1, false);

        InventoryManager.Instance.AddInventory(ItemType.Equip, body);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Hand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Head);
        InventoryManager.Instance.AddInventory(ItemType.Equip, Leg);

        InventoryManager.Instance.AddInventory(ItemType.Equip, rightHand);
        InventoryManager.Instance.AddInventory(ItemType.Equip, leftHand);
    }
}