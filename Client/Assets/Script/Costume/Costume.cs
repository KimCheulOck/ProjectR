public class Costume : IItem
{
    public ItemType ItemType { get { return ItemType.Costume; } }
    public int Count { get; private set; }
    public long SerialIndex { get; private set; }
    public string Thumbnail { get; private set; }

    public int Index;
    public CostumeType CostumeType;
    public string Name;
    public string[] Path;
    public bool IsWear;

    public Costume(long serialIndex, int index, string name, string[] path, string thumbnail, CostumeType costumeType)
    {
        SerialIndex = serialIndex;
        Index = index;
        Name = name;
        Path = path;
        Thumbnail = thumbnail;
        CostumeType = costumeType;
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