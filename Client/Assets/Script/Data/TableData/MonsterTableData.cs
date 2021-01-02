using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTableData", menuName = "ScriptableObjects/MonsterTableData", order = 1)]
public class MonsterTableData : BaseTableData
{
    public Status status;
    public string[] path;
}