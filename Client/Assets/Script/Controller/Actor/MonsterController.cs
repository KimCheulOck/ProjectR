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
        //monster.ChangeStatus(new Status());
        //monster.ChangeEquip(null);
    }

    #endregion
}
