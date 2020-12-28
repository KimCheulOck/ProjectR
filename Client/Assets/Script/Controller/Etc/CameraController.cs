using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField]
    private Camera mainCamera = null;
    public static Camera MainCamera { get { return Instance.mainCamera; } }

    private Transform mainCameraMoveTarget;

    public static void MainCameraMoveTarget(Transform target)
    {
        Instance.mainCameraMoveTarget = target;
    }

    private void Update()
    {
        if (mainCameraMoveTarget == null)
            return;

        if (mainCamera == null)
            return;

        mainCamera.transform.position = mainCameraMoveTarget.position;
    }

    //Camera.main.ScreenToWorldPoint(Input.mousePosition); 
}
