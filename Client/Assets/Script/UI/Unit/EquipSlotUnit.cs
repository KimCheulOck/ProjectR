using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject objActive = null;

    [SerializeField]
    private ItemSlotUnit loadPrefabs = null;

    private ItemSlotUnit itemSlotUnit = null;

    private IItem equip;

    public void SetSlot(IItem equip)
    {
        this.equip = equip;
    }

    public void Show()
    {
        CreateSlot();

        objActive.SafeSetActive(true);
        itemSlotUnit.SetItem(equip);
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
