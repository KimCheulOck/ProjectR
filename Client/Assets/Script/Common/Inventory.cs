public interface IItem
{
    ItemType ItemType { get; }
    int Count { get; }
    long SerialIndex { get; }
    string Thumbnail { get; }
}