using UnityEngine;

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
        OnKeyMove();
        OnKeyAttack();
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

        onKeyEventAttack(BodyType.Up, StateType.Idle);

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

    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
