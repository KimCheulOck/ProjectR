public enum ItemType
{
    Equip = 0,
    Potion,
    Matrial,
    Costume,
    Max,
}

public enum InventoryCategory
{
    [StringValue("전체")]
    ALL,

    [StringValue("장비")]
    Equip,

    [StringValue("소모품")]
    Item,

    [StringValue("기타")]
    Etc,
}