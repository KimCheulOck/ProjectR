using UnityEngine;

public class InstanceController : MonoSingleton<InstanceController>
{
    [SerializeField]
    private Transform objParent = null;
    public static T Create<T>(GameObject loadPrefab, Transform transform)
    {
        GameObject obj = Instantiate(loadPrefab, Instance.objParent);

        return obj.GetComponent<T>();
    }
}
