using UnityEngine;

[CreateAssetMenu(fileName = "EquipTableData", menuName = "ScriptableObjects/EquipTableData", order = 1)]
public class EquipTableData : BaseTableData
{
    // 스테이터스
    // 레벨
    // 장비 부위 (타입)
    // 세트 인덱스

    public Status status;
    public EquipType equipType;
    public WeaponType weaponType;
    public string[] path;
}