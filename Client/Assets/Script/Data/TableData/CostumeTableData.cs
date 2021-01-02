using UnityEngine;

[CreateAssetMenu(fileName = "CostumeTableData", menuName = "ScriptableObjects/CostumeTableData", order = 1)]
public class CostumeTableData : BaseTableData
{
    public CostumeType CostumeType;
    public string Name;
    public string[] Path;
}