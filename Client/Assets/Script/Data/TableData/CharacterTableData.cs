using UnityEngine;

[CreateAssetMenu(fileName = "CharacterTableData", menuName = "ScriptableObjects/CharacterTableData", order = 1)]
public class CharacterTableData : BaseTableData
{
    public Status status;
    public int[] DefaultCostumeIndex;
    public string path;
}