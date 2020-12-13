using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    #region 테스트 코드
    public Monster monster = null;

    public void Awake()
    {
        SetMonster();
    }

    public void SetMonster()
    {
        monster.ChangeStatus(new Status());
        monster.ChangeEquip(null);
    }

    #endregion




    [SerializeField]
    private float radius = 1;

    [SerializeField]
    private GameObject testObject1 = null;
    [SerializeField]
    private GameObject testObject2 = null;

    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log("1 : " + Vector3.Distance(testObject1.transform.position, testObject2.transform.position));
        Debug.Log("2 : " + Vector2.Distance(testObject1.transform.position, testObject2.transform.position));
    }
}
