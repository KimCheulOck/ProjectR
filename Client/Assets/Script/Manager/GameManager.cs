using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float SPEED = 100.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        FlowManager.Instance.ChangeFlow(new LogoFlow());
    }
}
