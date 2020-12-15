public class EquipTable : BaseTable<EquipTable, EquipTableData>
{
    // 스테이터스
    // 레벨
    // 장비 부위 (타입)
    // 세트 인덱스
    // 

    public Status status;
    public EquipType equipType;
}

public enum EquipType
{
    None=0,

    Head,

    Body,

    LeftHand,
    RightHand,

    Leg,

    Necklace,
    Earring,
    Ring,
    
    //Hat,
    //Hear_Top,
    //Eye,
    //Hear_Beck,
    //Mouse,
}