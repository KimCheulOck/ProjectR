using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : IItem
{
    public ItemType ItemType { get { return ItemType.Equip; } }
    public int Count { get; private set; }

    public Status status;
    public EquipType equipType;
    public WeaponType weaponType;
    public bool isWear;
    public string[] Path;

    public Equip(Status status, EquipType equipType, WeaponType weaponType, bool isWear, string[] path)
    {
        this.status = status;
        this.equipType = equipType;
        this.weaponType = weaponType;
        this.isWear = isWear;
        this.Path = path;
    }

    public void SetCount(int count)
    {
        Count = count;
    }
}
