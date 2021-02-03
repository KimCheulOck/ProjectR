public enum StatusType
{
    None = -1,
    [StringValue("체력")]
    Hp,
    [StringValue("마력")]
    Mp,
    [StringValue("공격력")]
    Attack,
    [StringValue("방어력")]
    Defense,
    [StringValue("명중률")]
    Hit,
    [StringValue("회피율")]
    Avoid,
    [StringValue("치명타확률")]
    Critical,
    [StringValue("공격속도")]
    AttackSpeed,
    [StringValue("이동속도")]
    MoveSpeed,
    [StringValue("시전속도")]
    SpellSpeed,
    [StringValue("원소력")]
    Elemental,
    Max
}