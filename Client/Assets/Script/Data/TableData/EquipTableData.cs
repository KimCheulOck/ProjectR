using UnityEngine;

[CreateAssetMenu(fileName = "EquipTableData", menuName = "ScriptableObjects/EquipTableData", order = 1)]
public class EquipTableData : BaseTableData
{
    // 스테이터스
    // 레벨
    // 장비 부위 (타입)
    // 세트 인덱스

    public Status Status;
    public EquipType EquipType;
    public WeaponType WeaponType;
    public string[] Path;
    public string Thumbnail;
}