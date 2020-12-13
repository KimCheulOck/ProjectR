using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceController : MonoSingleton<InstanceController>
{
    public static T Create<T>(GameObject loadPrefab, Transform transform)
    {
        GameObject obj = Instantiate(loadPrefab, Instance.transform);

        return obj.GetComponent<T>();
    }
}
