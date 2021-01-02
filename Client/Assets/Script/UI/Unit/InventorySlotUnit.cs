using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject objActive = null;

    [SerializeField]
    private ItemSlotUnit loadPrefabs = null;

    private ItemSlotUnit itemSlotUnit = null;

    private IItem item;

    public void SetSlot(IItem item)
    {
        this.item = item;
    }

    public void Show()
    {
        CreateSlot();

        objActive.SafeSetActive(true);

        itemSlotUnit.SetItem(item);
        itemSlotUnit.SetSlotType(ItemSlotUnit.SlotType.Inventory);
        itemSlotUnit.Show();
    }

    public void Empty()
    {
        objActive.SafeSetActive(false);
    }

    private void CreateSlot()
    {
        if (itemSlotUnit != null)
            return;

        itemSlotUnit = Instantiate(loadPrefabs, objActive.transform);
    }
}
