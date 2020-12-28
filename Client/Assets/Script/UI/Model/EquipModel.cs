using UnityEngine;

public class EquipModel
{
    public IItem[] wearEquips;
    public int categoryIndex;

    public void SetCategory(int categoryIndex)
    {
        this.categoryIndex = categoryIndex;
        wearEquips = EquipManager.Instance.GetEquipList((EquipCategory)categoryIndex);

        PlayerPrefs.SetInt("EquipCategory", categoryIndex);
    }
}