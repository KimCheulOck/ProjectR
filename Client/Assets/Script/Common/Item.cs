using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    public ItemType ItemType { get { return ItemType.Potion; } }
    public int Count { get; private set; }
    // 포션, 재료, 
}
