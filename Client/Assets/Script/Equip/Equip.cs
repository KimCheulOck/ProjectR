using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : IItem
{
    public ItemType ItemType { get { return ItemType.Equip; } }
    public int Count { get; private set; }

    public EquipType equipType;
    public Status status;
    public bool isWear;
    public string[] path;

    public Equip(EquipType equipType, Status status, bool isWear, string[] path)
    {
        this.equipType = equipType;
        this.status = status;
        this.isWear = isWear;
        this.path = path;
    }

    public void SetCount(int count)
    {
        this.Count = count;
    }
}
