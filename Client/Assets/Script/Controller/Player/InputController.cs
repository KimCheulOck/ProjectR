﻿using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    private const int MOUSELEFT = 0;
    private const int MOUSERIGHT = 1;

    public delegate void OnKeyEvent(BodyType bodyType, StateType stateType, params object[] extraData);
    public delegate bool OnKeyDelayEvent(BodyType bodyType);
    public static OnKeyEvent onKeyEventMove = null;
    public static OnKeyEvent onKeyEventAttack = null;
    public static OnKeyDelayEvent onKeyEventWaiting = null;

    private void Update()
    {
        OnKeyUI();
        OnKeyMove();

        if (UIController.IsOpenUI())
        {
            OnKeyUIFocus();
        }
        else
        {
            OnKeyAttack();
        }
    }

    private void OnKeyUI()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 플레이어 정보
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // 옵션
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            // 인벤토리
            if (!DisableUI(UIPrefabs.InventoryView))
                UIController.Enter(new InventoryPresenter());
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            // 장비
            if (!DisableUI(UIPrefabs.EquipView))
                UIController.Enter(new EquipPresenter());
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            // 우편함
        }
        //else if (Input.GetKeyDown(KeyCode.T))
        //{
        //}
        //else if (Input.GetKeyDown(KeyCode.L))
        //{
        //}
        else if (Input.GetKeyDown(KeyCode.K))
        {
            // 스킬
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            // 퀘스트
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            // 길드
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            // 맵
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            // 친구
        }
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //}
    }

    private void OnKeyUIFocus()
    {
        bool isMouseClick = false;
        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetMouseButtonDown(i))
            {
                isMouseClick = true;
                break;
            }
        }

        if (!isMouseClick)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 위치를 Ray로 반환
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Cube Clicked");
                Transform target = hit.collider.transform.parent;
                while (true)
                {
                    if (target == null)
                        return;

                    BaseView baseView = target.GetComponent<BaseView>();
                    if (baseView == null)
                    {
                        target = target.parent;
                        continue;
                    }

                    UIController.FocusSort(UIController.GetPresenter(baseView.GetHashCode()));
                    return;
                }

                //UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            }
        }
    }

    private void OnKeyMove()
    {
        if (onKeyEventMove == null)
            return;

        DirectionType heightDirectionType = DirectionType.None;
        DirectionType widthDirectionType = DirectionType.None;
        if (Input.GetKey(KeyCode.W))
            heightDirectionType = DirectionType.Up;
        else if (Input.GetKey(KeyCode.S))
            heightDirectionType = DirectionType.Down;
        if (Input.GetKey(KeyCode.A))
            widthDirectionType = DirectionType.Left;
        else if (Input.GetKey(KeyCode.D))
            widthDirectionType = DirectionType.Right;

        if (heightDirectionType == DirectionType.None && widthDirectionType == DirectionType.None)
            onKeyEventMove(BodyType.Low, StateType.Idle);
        else
            onKeyEventMove(BodyType.Low, StateType.Move, heightDirectionType, widthDirectionType);
    }

    private void OnKeyAttack()
    {
        if (onKeyEventAttack == null)
            return;

        if (onKeyEventWaiting(BodyType.Up))
            return;

        //onKeyEventAttack(BodyType.Up, StateType.Idle);

        if (Input.GetMouseButton(MOUSELEFT))
        {
            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onKeyEventAttack(BodyType.Up, StateType.Attack);
        }
        else if (Input.GetMouseButton(MOUSERIGHT))
        {
        }
        else
        {
            onKeyEventAttack(BodyType.Up, StateType.Idle);
        }
    }

    private bool DisableUI(UIPrefabs uiPrefabs)
    {
        if (UIController.IsOpenVIew(uiPrefabs))
        {
            UIController.Exit(UIController.GetPresenter(uiPrefabs));
            return true;
        }

        return false;
    }
    
    //private static float GetAngle(Vector3 vStart, Vector3 vEnd)
    //{
    //    Vector3 v = vEnd - vStart;

    //    return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    //}
}
