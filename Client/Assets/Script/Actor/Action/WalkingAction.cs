using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAction : Action
{
    private const int DIRECTION_MAX = 2;
    private DirectionType[] directionTypes = new DirectionType[DIRECTION_MAX];

    public WalkingAction(Actor actor, ActionType actionType) : base(actor, actionType)
    {
    }

    public override void Initialize(BodyType bodyType, params object[] extraData)
    {
        base.Initialize(bodyType, extraData);

        for (int i = 0; i < directionTypes.Length; ++i)
            directionTypes[i] = (DirectionType)extraData[i];
    }

    public override void Start()
    {
        float speed = actor.Status.moveSpeed * Time.deltaTime;// * GameManager.SPEED;
        for (int i = 0; i < directionTypes.Length; ++i)
        {
            switch (directionTypes[i])
            {
                case DirectionType.Left:
                    {
                        actor.transform.Translate(Vector2.left * speed);
                    }
                    break;

                case DirectionType.Right:
                    {
                        actor.transform.Translate(Vector2.right * speed);
                    }
                    break;

                case DirectionType.Up:
                    {
                        actor.transform.Translate(Vector2.up * speed);
                    }
                    break;

                case DirectionType.Down:
                    {
                        actor.transform.Translate(Vector2.down * speed);
                    }
                    break;
            }
        }
    }
}
