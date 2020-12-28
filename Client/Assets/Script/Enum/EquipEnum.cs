public enum EquipType
{
    None = -1,

    LeftWeapon,
    RightWeapon,

    Head,

    Body,

    Hand,

    Leg,

    Necklace,
    Earring,
    Ring,

    Max,

    //Hat,
    //Hear_Top,
    //Eye,
    //Hear_Beck,
    //Mouse,
}

public enum WeaponType
{
    None = 0,
    Axe,
    Bow,
    Etc,
    Gun,
    Hammer,
    Projectile,
    Spear,
    Sword,
    Wand,
}

public enum EquipCategory
{
    [StringValue("장비")]
    Equip,

    [StringValue("코스튬")]
    Costume,

    [StringValue("펫")]
    Pat,
}