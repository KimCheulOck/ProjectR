using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EquipManager.Instance.TestSetWearEquip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
