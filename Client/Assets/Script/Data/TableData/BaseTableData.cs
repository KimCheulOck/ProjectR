using UnityEngine;

public class BaseTableData : ScriptableObject
{
    [SerializeField]
    private int index;
    public int Index { get { return index; } }
}