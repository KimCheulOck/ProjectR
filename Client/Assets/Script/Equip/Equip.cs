public class Equip : IItem
{
    public ItemType ItemType { get { return ItemType.Equip; } }
    public int Count { get; private set; }
    public long SerialIndex { get; private set; }
    public string Thumbnail { get; private set; }    

    public Status Status;
    public EquipType EquipType;
    public WeaponType WeaponType;
    public bool IsWear;
    public string[] Path;
    public int Index;

    public Equip(long serialIndex, int index, string[] path, string thumbnail, Status status, EquipType equipType, WeaponType weaponType)
    {
        SerialIndex = serialIndex;
        Index = index;

        Path = path;
        Thumbnail = thumbnail;

        Status = status;
        EquipType = equipType;
        WeaponType = weaponType;
    }

    public void SetCount(int count)
    {
        Count = count;
    }

    public void SetWear(bool isWear)
    {
        IsWear = isWear;
    }
}
