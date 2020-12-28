using UnityEngine;
using System.Collections.Generic;

public class BaseTable<T, TT> : Singleton<T> where T : class where TT : BaseTableData
{
    protected List<TT> Datas = new List<TT>();

    public void LoadTable()
    {
        string path = string.Format("Data/Table/{0}", Instance.GetType().Name);
        object[] tableDataList = Resources.LoadAll(path);

        Datas.Clear();
        for (int i = 0; i < tableDataList.Length; ++i)
        {
            TT t = tableDataList[i] as TT;
            Datas.Add(t);
        }
    }

    public TT FindToIndex(int index)
    {
        return Datas.Find(match => match.Index == index);
    }

    public List<TT> FindAll()
    {
        return Datas;
    }
}