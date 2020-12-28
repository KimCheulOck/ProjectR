using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotGroupUnit : MonoBehaviour
{
    [SerializeField]
    private InventorySlotUnit[] inventoryUnits = null;

    public int childUnitCount { get { return inventoryUnits == null ? 0 : inventoryUnits.Length; } }

    public void SetSlotUnit(int index, IItem item)
    {
        inventoryUnits[index].SetSlot(item);
        inventoryUnits[index].Show();
    }

    public void EmptySlotUnit(int index)
    {
        inventoryUnits[index].SetSlot(null);
        inventoryUnits[index].Empty();
    }
}
